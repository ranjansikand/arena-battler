// Script that spawns enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] List<GameObject> _commonEnemies = new List<GameObject>();
    [SerializeField] List<GameObject> _rareEnemies = new List<GameObject>();


    [Header("Wave Control Variables")]
    [SerializeField] List<Vector2> _spawnPoints = new List<Vector2>();
    [SerializeField, Range(0,5)] float _minDelayBetweenEnemies, _maxDelayBetweenEnemies;
    [SerializeField, Range(0,30)] float _waveDelay;
    [SerializeField] int _startingEnemyCount;
    WaitForSeconds _timeBetweenWaves;
    int _enemiesThisWave, _waveCount = 1;



    void Start()
    {
        _timeBetweenWaves = new WaitForSeconds(_waveDelay);
        _enemiesThisWave = _startingEnemyCount;

        StartCoroutine(EnemyWaveManagerRoutine());
    }

    IEnumerator EnemyWaveManagerRoutine() {
        while (true) {
            // Delay before the start of the next wave
            yield return _timeBetweenWaves;
            // Text saying the number of the upcoming wave

            for (int i = 0; i < _enemiesThisWave; i++) {
                // Choose a random spawn location
                Vector2 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                // Use more difficult enemies as the game progresses
                GameObject enemyToSpawn = Random.Range(1, 20) < _waveCount ? 
                    _rareEnemies[Random.Range(0, _rareEnemies.Count)] :
                    _commonEnemies[Random.Range(0, _commonEnemies.Count)];
                
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(Random.Range(
                    _minDelayBetweenEnemies, 
                    _maxDelayBetweenEnemies));
            }

            // Prepare the next wave
            _enemiesThisWave = Mathf.FloorToInt(1.5f * _enemiesThisWave);
            _waveCount++;
        }
    }

    public void EndGame()
    {
        StopAllCoroutines();

        // Display Game Over text
    }
}
