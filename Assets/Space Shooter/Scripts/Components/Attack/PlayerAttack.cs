using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using SpaceShooter.Powerups;
using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Attack
{
    public class PlayerAttack : MonoBehaviour
    {        
        private ObjType _attacker;

        [SerializeField]
        private GameObject _primaryWeapon;
        [SerializeField, ReadOnly]
        private GameObject _activeWeapon;
        private GameObject _currentlyFired;

        private Vector3 _currentPos;
        [SerializeField]
        private float _primaryOffset = 0.85f;

        [SerializeField]
        private float _fireRate = 0.25f;
        private WaitForSeconds _reloadTime;
        private bool _canFire;        

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

            UpdateReloadTime(_fireRate);

            if (_primaryWeapon != null)
            {
                _activeWeapon = _primaryWeapon;
            }
            else
            {
                Debug.LogError($"Assign Primary Weapon on {this.GetType().Name} for {transform.name}.");
            }
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

            if (!_isTrippleShotAcive)
            {
                _currentPos.y += _primaryOffset;
            }

            _currentlyFired = Instantiate(_activeWeapon, _currentPos, Quaternion.identity);
            foreach (Transform child in _currentlyFired.transform)
            {
                if (child.TryGetComponentInParents(out ICollidable collidable))
                {
                    collidable.Type = ObjType.Player;
                }
            }

            StartCoroutine(FireRateRoutine());
        }

        private IEnumerator FireRateRoutine()
        {
            _canFire = false;

            yield return _reloadTime;

            _canFire = true;
        }

        private void UpdateReloadTime(float fireRate)
        {
            _reloadTime = new WaitForSeconds(fireRate);

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
