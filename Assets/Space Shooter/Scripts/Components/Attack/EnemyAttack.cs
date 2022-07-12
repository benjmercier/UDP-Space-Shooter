using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.Attack
{
    public class EnemyAttack : Attack
    {
        [SerializeField]
        private float _minReloadTime = 3f;
        [SerializeField]
        private float _maxReloadTime = 7f;
        private float _randomReloadTime;

        protected override void Update()
        {
            if (_canFire)
            {
                CalculateAttack();
            }            
        }

        protected override void CalculateAttack()
        {
            _currentPos = transform.position;

            _currentlyFired = Instantiate(_activeWeapon, _currentPos, Quaternion.identity);

            foreach (Transform child in _currentlyFired.transform)
            {
                if (child.TryGetComponentInParents(out ICollidable collidable))
                {
                    collidable.Type = ObjType.Enemy;
                }
            }

            if (TryGetComponent(out IAudible audible))
            {
                audible.PlayOneShot(_primaryWeaponAudioClip);
            }

            StartCoroutine(FireRateRoutine());
        }

        protected override IEnumerator FireRateRoutine()
        {
            _canFire = false;

            _randomReloadTime = Random.Range(_minReloadTime, _maxReloadTime);

            _reloadTime = new WaitForSeconds(_randomReloadTime);

            yield return _reloadTime;

            _canFire = true;
        }
    }
}
