using SpaceShooter.Interfaces;
using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        [Tooltip("Set Max Lives to 0 if any damage will destroy the object.")]
        [Min(0)]
        private int _maxLives;
        [SerializeField] 
        [ReadOnly]
        private int _remainingLives;

        private void Start()
        {
            ResetCurrentLives();
        }

        // look into
        private void OnValidate()
        {
            
        }

        private void ResetCurrentLives()
        {
            _remainingLives = _maxLives;
        }

        public void Damage()
        {
            if (_remainingLives > 0)
            {
                _remainingLives--;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
