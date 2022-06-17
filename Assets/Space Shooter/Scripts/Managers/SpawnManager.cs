using SpaceShooter.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField]
        private GameObject _spawnPrefab;
        [SerializeField]
        private int _amountToSpawn = 5;
        private int _currentSpawn = 0;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(SpawnRoutine());
            }
        }

        private IEnumerator SpawnRoutine()
        {
            while (_currentSpawn < _amountToSpawn)
            {
                var obj = Instantiate(_spawnPrefab);
                obj.transform.position = new Vector3(Random.Range(-5, 5), 7, 0);

                _currentSpawn++;

                yield return new WaitForSeconds(2.5f);
            }

            _currentSpawn = 0;
        }
    }
}
