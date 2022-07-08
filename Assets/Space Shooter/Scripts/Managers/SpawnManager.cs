using SpaceShooter.Components;
using SpaceShooter.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Managers
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField]
        private GameObject _spawnPrefab;
        [SerializeField]
        private int _amountToSpawn = 5;
        private int _currentSpawn = 0;

        [SerializeField]
        private List<GameObject> _powerupPrefabs;

        private bool _canSpawn;

        private void OnEnable()
        {
            GameManager.onStartGame += StartSpawning;
            GameManager.onGameOver += StopSpawning;
        }

        private void OnDisable()
        {
            GameManager.onStartGame -= StartSpawning;
            GameManager.onGameOver -= StopSpawning;
        }

        private void StartSpawning()
        {
            StartCoroutine(StartSpawningRoutine());
        }

        private IEnumerator StartSpawningRoutine()
        {
            yield return new WaitForSeconds(3f);

            _canSpawn = true;

            StartCoroutine(SpawnRoutine());
            StartCoroutine(SpawnPowerupRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (_currentSpawn < _amountToSpawn)
            {
                if (!_canSpawn)
                {
                    break;
                }

                var obj = Instantiate(_spawnPrefab);
                obj.transform.position = new Vector3(Random.Range(-5, 5), 7, 0);
                obj.transform.parent = this.transform;

                _currentSpawn++;

                yield return new WaitForSeconds(2.5f);
            }

            _currentSpawn = 0;
        }

        private IEnumerator SpawnPowerupRoutine()
        {
            yield return new WaitForSeconds(3f);

            while (_canSpawn)
            {
                int powerup = Random.Range(0, _powerupPrefabs.Count);

                var obj = Instantiate(_powerupPrefabs[powerup]);
                obj.transform.position = new Vector2(Random.Range(-5, 5), 7);
                obj.transform.parent = this.transform;

                float random = Random.Range(3f, 7f);
                yield return new WaitForSeconds(random);
            }
        }

        private void StopSpawning()
        {
            _canSpawn = false;
        }
    }
}
