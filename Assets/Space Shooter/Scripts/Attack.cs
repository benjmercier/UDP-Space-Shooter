using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        private GameObject _weaponPrefab;

        private Vector3 _currentPos;
        [SerializeField]
        private float _offset = 0.85f;

        [SerializeField]
        private float _fireRate = 0.25f;
        private bool _canFire;

        private WaitForSeconds _waitTime;

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
            _currentPos.y += _offset;

            Instantiate(_weaponPrefab, _currentPos, Quaternion.identity);

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
