using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.Attack
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _primaryWeapon;
        [SerializeField]
        protected AudioClip _primaryWeaponAudioClip;
        [SerializeField, ReadOnly]
        protected GameObject _activeWeapon;
        protected GameObject _currentlyFired;

        protected Vector3 _currentPos;
        [SerializeField]
        protected float _primaryOffset = 0.85f;

        [SerializeField]
        protected float _fireRate = 0.25f;
        protected WaitForSeconds _reloadTime;
        protected bool _canFire;

        protected virtual void Start()
        {
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

        protected abstract void Update();

        protected abstract void CalculateAttack();

        protected virtual IEnumerator FireRateRoutine()
        {
            _canFire = false;

            yield return _reloadTime;

            _canFire = true;
        }

        protected virtual void UpdateReloadTime(float fireRate)
        {
            _reloadTime = new WaitForSeconds(fireRate);

            _canFire = true;
        }
    }
}
