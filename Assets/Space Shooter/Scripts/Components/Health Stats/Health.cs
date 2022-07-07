using SpaceShooter.Interfaces;
using SpaceShooter.PropertyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.Stats
{
    [RequireComponent(typeof(CollisionDetection))]
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        [Tooltip("Set Max Lives to 0 if any collision/damage will destroy the object.")]
        [Min(0)]
        protected int _maxLives;
        [SerializeField] 
        [ReadOnly]
        protected int _remainingLives;

        protected int _points;

        public static event Action<int> onObjDestroyed;

        protected virtual void Start()
        {
            ResetCurrentLives();
        }

        // look into
        protected void OnValidate()
        {
            
        }

        protected void ResetCurrentLives()
        {
            _remainingLives = _maxLives;
        }

        public virtual void Damage()
        {
            if (_remainingLives > 0)
            {
                _remainingLives--;
            }
            else
            {
                _points = 0;

                if (TryGetComponent(out IScorable scorable))
                {
                    _points = scorable.PointsToAward;
                }

                if (TryGetComponent(out IAnimatable animatable))
                {
                    animatable.PlayDestroyedAnim();
                }
                
                Destroy(this.gameObject);
            }
        }

        protected void OnObjDestroyed(int points)
        {
            onObjDestroyed?.Invoke(points);
        }
    }
}
