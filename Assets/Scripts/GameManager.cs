// Script that spawns enemies, controls game over, and allows restarting

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple inventories found!");
            return;
        }

        instance = this;

        _playerInput = new PlayerControls();
        _playerInput.General.Pause.performed += OnPausePressed;
        _playerInput.General.Quit.performed += OnQuitPressed;

        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
    }
    #endregion

    PlayerControls _playerInput;

    [Header("--- Enemies ---")]
    [SerializeField] List<GameObject> _commonEnemies = new List<GameObject>();


    [Header("--- Wave Control Variables ---")]
    [SerializeField] List<Vector2> _spawnPoints = new List<Vector2>();
    [SerializeField, Range(0,5)] float _minDelayBetweenEnemies, _maxDelayBetweenEnemies;
    [SerializeField, Range(0,30)] float _waveDelay;
    [SerializeField] int _startingEnemyCount;
    WaitForSeconds _timeBetweenWaves;
    int _enemiesThisWave, _waveCount = 1;

    [Header("---- UI Fields ----")]
    [SerializeField] GameObject _playerUI;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] Image _gameOverScreen;
    [SerializeField] Text _scoreCounter;
    [SerializeField] Text _waveCounter;
    [SerializeField] Text _highScoreText;
    [SerializeField] Slider _volumeControl;

    [Header("---- Audio ----")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _knightFootstep;
    [SerializeField] AudioSource _princessFootstep;
    [SerializeField] AudioClip[] _backgroundTracks;
    [SerializeField] AudioClip _gameOverSound;
    [SerializeField] AudioClip _highScoreSound;

    float _masterVolume;

    // Score tracking
    int _enemiesDefeated = 0;
    int _minimumScoreBeforeNextWave = -1;
    int _highScore;

    // Conditions
    bool _paused = false;
    bool _gameOver = false;



    void Start()
    {
        if (_pauseMenu == null) _pauseMenu = GameObject.Find("PauseMenu");
        if (_gameOverScreen == null) _gameOverScreen = GameObject.Find("GameOver").GetComponent<Image>();

        // Set up variables
        _timeBetweenWaves = new WaitForSeconds(_waveDelay);
        _enemiesThisWave = _startingEnemyCount;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Set up audio
        _volumeControl.value = _masterVolume;
        UpdateVolume();

        // Set up UI
        _playerUI.SetActive(true);
        _pauseMenu.SetActive(false);
        _gameOverScreen.gameObject.SetActive(false);

        // Start UI
        _scoreCounter.text = "0";
        _waveCounter.text = "Enemies incoming!";
        StartCoroutine(WaveManagerRoutine());
    }

    void Update() {
        if (!_gameOver && !_musicSource.isPlaying) {
            _musicSource.clip = _backgroundTracks[Random.Range(0, _backgroundTracks.Length)];
            _musicSource.Play();
        }
    }

    // Input Actions
    private void OnEnable() {
        _playerInput.Enable();
    }

    private void OnDisable() {
        _playerInput.Disable();
    }
    private void OnPausePressed(InputAction.CallbackContext context) {
        if (_paused) ResumeGame();
        else PauseGame();
    }

    private void OnQuitPressed(InputAction.CallbackContext context) {
        CheckHighScore();

        // Quit game
        Application.Quit();
    }

    // Enemy Spawning
    IEnumerator WaveManagerRoutine()
    {
        while (!_gameOver) {
            // Wait while enemies are still alive
            while (_enemiesDefeated < _minimumScoreBeforeNextWave) yield return null;

            // Prepare this wave
            yield return _timeBetweenWaves;
            Debug.Log("Wave " + _waveCount + " Begins!");
            _waveCounter.text = "Wave " + _waveCount;

            // Execute this wave
            StartCoroutine(EnemySpawnRoutine());
            _minimumScoreBeforeNextWave += _enemiesThisWave;

            // Prepare the next wave
            Debug.Log("Wave " + _waveCount + " Spawning Complete");
            _enemiesThisWave = Mathf.FloorToInt(_enemiesThisWave * 1.25f);
            _waveCount++;
        }
    }

    IEnumerator EnemySpawnRoutine() {
        for (int i = 0; i < _enemiesThisWave; i++) {
            Instantiate(
                SelectEnemy(),  
                _spawnPoints[Random.Range(0, _spawnPoints.Count)], 
                Quaternion.identity
            );

            yield return new WaitForSeconds(Random.Range(
                _minDelayBetweenEnemies, 
                _maxDelayBetweenEnemies));
        }
    }

    GameObject SelectEnemy() {
        int chance = Random.Range(1, 11);
        if (chance < 6) {
            // guaranteed weak one
            return _commonEnemies[0];
        }
        else {
            // Any random enemy
            return _commonEnemies[Random.Range(0, _commonEnemies.Count)];
        }
    }

    public void EndGame()
    {
        _gameOver = true;
        StopAllCoroutines();
        

        if (CheckHighScore()) {
            _highScoreText.text = "New high score: " + _enemiesDefeated;
            _musicSource.clip = _highScoreSound;
        } else {
            _highScoreText.text = "High Score: " + _highScore;
            _musicSource.clip = _gameOverSound;
        }
        _musicSource.Play();

        // Display Game Over text
        _gameOverScreen.gameObject.SetActive(true);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver() {
        for (int i = 0; i < 255; i++) {
            var tempColor = _gameOverScreen.color;
            tempColor.a = 1.0f * i / 255f;

            _gameOverScreen.color = tempColor;
        
            yield return null;
        }
    }

    public void AddScore()
    {
        _enemiesDefeated++;
        _scoreCounter.text = _enemiesDefeated.ToString();
    }

    bool CheckHighScore() {
        if (_enemiesDefeated > _highScore) {
            PlayerPrefs.SetInt("HighScore", _enemiesDefeated);
            return true;
        }
        return false;
    }

    void ResumeGame() {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _playerUI.SetActive(true);

        _paused = false;
    }

    void PauseGame() {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _playerUI.SetActive(false);

        _paused = true;
    }

    public float CurrentMasterVolume() {
        return _masterVolume;
    }

    // Button Actions
    public void Restart() {
        SceneManager.LoadScene(1);  // Restart this scene
    }

    public void Quit() {
        SceneManager.LoadScene(0);  // Go to title menu
    }

    public void UpdateVolume() {
        _masterVolume = _volumeControl.value;
        _musicSource.volume = _masterVolume * 0.5f;
        _knightFootstep.volume = _princessFootstep.volume = _masterVolume;
        PlayerPrefs.SetFloat("MasterVolume", _masterVolume);
    }
}
