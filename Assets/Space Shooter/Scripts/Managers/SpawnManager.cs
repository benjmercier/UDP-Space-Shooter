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
            Health.onObjDestroyed += OnPlayerDestroyed;
        }

        private void OnDisable()
        {
            Health.onObjDestroyed -= OnPlayerDestroyed;
        }

        // Start is called before the first frame update
        void Start()
        {
            _canSpawn = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _canSpawn)
            {
                StartCoroutine(SpawnRoutine());
                StartCoroutine(SpawnPowerupRoutine());
            }
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

        private void OnPlayerDestroyed(string objTag)
        {
            if (objTag == "Player")
                _canSpawn = false;
        }
    }
}
