using SpaceShooter.Interfaces;
using SpaceShooter.PropertyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        [Tooltip("Set Max Lives to 0 if any collision/damage will destroy the object.")]
        [Min(0)]
        private int _maxLives;
        [SerializeField] 
        [ReadOnly]
        private int _remainingLives;

        private string _objTag;

        public static event Action<string> onObjDestroyed;

        private void Start()
        {
            _objTag = transform.root.tag;

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
                OnObjDestroyed(_objTag);
                Destroy(this.gameObject);
            }
        }

        private void OnObjDestroyed(string objTag)
        {
            onObjDestroyed?.Invoke(objTag);
        }
    }
}
