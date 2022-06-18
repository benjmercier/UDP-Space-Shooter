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

        private void OnPlayerDestroyed(string objTag)
        {
            if (objTag == "Player")
                _canSpawn = false;
        }
    }
}
