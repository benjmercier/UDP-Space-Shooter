using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using SpaceShooter.Powerups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(ICollidable))]
    public class Attack : MonoBehaviour
    {        
        private Category _attacker;

        [SerializeField]
        private List<GameObject> _weaponPrefabs;
        private GameObject _currentWeapon;

        private Vector3 _currentPos;
        [SerializeField]
        private float _offset = 0.85f;

        [SerializeField]
        private float _fireRate = 0.25f;
        private bool _canFire;

        private WaitForSeconds _waitTime;

        [SerializeField]
        private bool _isTrippleShotAcive = false;

        private Coroutine _trippleShotPowerDownRoutine;

        private void OnEnable()
        {
            Powerup.onActivateTrippleShot += ActivateTrippleShot;
        }

        private void OnDisable()
        {
            Powerup.onActivateTrippleShot -= ActivateTrippleShot;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (transform.TryGetComponentInParents(out ICollidable collidable))
            {
                _attacker = collidable.Type;
            }

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

            _currentWeapon = Instantiate(_weaponPrefabs[prefab], _currentPos, Quaternion.identity);
            foreach (Transform child in _currentWeapon.transform)
            {
                if (child.TryGetComponent(out ICollidable collidable))
                {
                    collidable.Type = _attacker;
                }
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

        private void ActivateTrippleShot()
        {
            if (_trippleShotPowerDownRoutine != null)
            {
                StopCoroutine(_trippleShotPowerDownRoutine);
            }

            _isTrippleShotAcive = true;

            _trippleShotPowerDownRoutine = StartCoroutine(TrippleShotPowerDownRoutine());
        }

        private IEnumerator TrippleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5f);

            _isTrippleShotAcive = false;
            _trippleShotPowerDownRoutine = null;
        }
    }
}
