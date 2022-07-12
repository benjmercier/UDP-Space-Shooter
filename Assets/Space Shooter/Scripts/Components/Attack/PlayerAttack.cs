using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using SpaceShooter.Powerups;
using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.Attack
{
    public class PlayerAttack : Attack
    {
        [SerializeField]
        private List<GameObject> _weaponUpgrades;

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

        // Update is called once per frame
        protected override void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Space) && _canFire)
            {
                CalculateAttack();
            }
        }

        protected override void CalculateAttack()
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

            if (TryGetComponent(out IAudible audible))
            {
                audible.PlayOneShot(_primaryWeaponAudioClip);
            }

            StartCoroutine(FireRateRoutine());
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
            _activeWeapon = _weaponUpgrades[0];

            yield return new WaitForSeconds(5f);

            _isTrippleShotAcive = false;
            _trippleShotPowerDownRoutine = null;

            _activeWeapon = _primaryWeapon;
        }
    }
}
