// Script that powers the title screen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    PlayerControls _playerInput;
    AudioSource _audiosource;

    [Header("Menus")]
    [SerializeField] GameObject _titleMenu;
    [SerializeField] GameObject _controlsMenu;

    [Header("Audio")]
    [SerializeField] Slider _volumeControl;
    [SerializeField] AudioClip _confirmation, _back, _close;
    [SerializeField] AudioSource _backgroundMusicSource;


    bool _controlsMenuActive = false;
    float _masterVolume;

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
        _playerInput = new PlayerControls();

        _playerInput.General.Quit.performed += OnQuitPressed;

        _titleMenu.SetActive(true);
        _controlsMenu.SetActive(false);

        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        _volumeControl.value = _masterVolume;
    }

    private void OnQuitPressed(InputAction.CallbackContext context) {
        QuitGame();
    }   

    public void StartGame() {
        _audiosource.PlayOneShot(_confirmation, _masterVolume);

        Invoke(nameof(PlayGame), 0.5f);
    }

    public void Quit() {
        _audiosource.PlayOneShot(_close, _masterVolume);

        Invoke(nameof(QuitGame), 0.5f);
    }

    public void Controls() {
        _controlsMenuActive = !_controlsMenuActive;

        if (_controlsMenuActive) _audiosource.PlayOneShot(_confirmation, _masterVolume);
        else _audiosource.PlayOneShot(_back, _masterVolume);

        _controlsMenu.SetActive(_controlsMenuActive);
        _titleMenu.SetActive(!_controlsMenuActive);
    }

    public void SetVolume() {
        _masterVolume = _volumeControl.value;
        _backgroundMusicSource.volume = _masterVolume;
        PlayerPrefs.SetFloat("MasterVolume", _masterVolume);
    }

    private void PlayGame() {
        SceneManager.LoadScene(1);
    }

    private void QuitGame() {
        Application.Quit();
    }
}
