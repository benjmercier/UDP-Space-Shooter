using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Attack : MonoBehaviour, IAttackable
    {
        [SerializeField]
        private Category _attacker;
        public Category Attacker { get => _attacker; set => _attacker = value; }

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

            var obj = Instantiate(_weaponPrefabs[prefab], _currentPos, Quaternion.identity);
            var fireable = obj.GetComponentsInChildren<IAttackable>();

            foreach (var weapon in fireable)
            {
                weapon.Attacker = _attacker;
            }

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
