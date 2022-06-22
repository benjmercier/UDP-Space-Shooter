using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _weaponPrefabs;

        private Vector3 _currentPos;
        [SerializeField]
        private float _offset = 0.85f;

        [SerializeField]
        private float _fireRate = 0.25f;
        private bool _canFire;

        private WaitForSeconds _waitTime;

        [SerializeField]
        private bool _isTrippleShotAcive = false;

        // Start is called before the first frame update
        void Start()
        {
            UpdateWaitTime(_fireRate);
        }

        // Update is called once per frame
        void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Space) && _canFire)
            {
                CalculateAttack();
            }
        }

        private void CalculateAttack()
        {
            _currentPos = transform.position;
            int prefab;

            if (!_isTrippleShotAcive)
            {
                _currentPos.y += _offset;
                prefab = 0;
            }
            else
            {
                prefab = 1;
            }

            Instantiate(_weaponPrefabs[prefab], _currentPos, Quaternion.identity);

            StartCoroutine(FireRateRoutine());
        }

        private IEnumerator FireRateRoutine()
        {
            _canFire = false;

            yield return _waitTime;

            _canFire = true;
        }

        private void UpdateWaitTime(float rate)
        {
            _waitTime = new WaitForSeconds(rate);

            _canFire = true;
        }
    }
}
