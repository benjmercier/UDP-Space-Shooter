using SpaceShooter.Interfaces;
using SpaceShooter.Powerups;
using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components
{
    public class Shield : MonoBehaviour, IProtectable
    {
        [SerializeField]
        private GameObject _shieldObj;
        public GameObject ShieldObj => _shieldObj;
        [SerializeField]
        private bool _shieldActive;
        public bool ShieldActive => _shieldActive;

        [SerializeField]
        private Collider2D _mainCollider;

        [SerializeField]
        private int _shieldHealth = 3;
        [SerializeField]
        [ReadOnly]
        private int _currentShieldHealth;

        private void OnEnable()
        {
            Powerup.onActivateShield += ActivateShield;
        }

        private void OnDisable()
        {
            Powerup.onActivateShield -= ActivateShield;
        }

        private void Start()
        {
            if (_shieldObj == null)
            {
                _shieldObj = transform.Find("Shield").gameObject;
            }
            else
            {
                //Debug.LogError($"Shield object has not been added as a child to {transform.name}");
            }

            _shieldObj.SetActive(false);
            _currentShieldHealth = _shieldHealth;
        }

        private void ActivateShield()
        {
            if (!_shieldActive)
            {
                ToggleShields(true);
            }
        }

        /// <summary>
        /// if shield on, then turn off main collider
        /// if shield off, then turn on main collider
        /// </summary>
        private void ToggleShields(bool shieldActive)
        {
            _shieldActive = shieldActive;
            _shieldObj.SetActive(shieldActive);
            _mainCollider.enabled = !shieldActive;
        }

        public void ShieldHit()
        {
            _currentShieldHealth--;

            if (_currentShieldHealth <= 0)
            {
                _shieldActive = false;

                ToggleShields(_shieldActive);
            }
        }
    }
}
