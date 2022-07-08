using SpaceShooter.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.HealthStats
{
    public class PlayerHealth : Health
    {
        public static event Action<int> onPlayerHit;
        public static event Action onPlayerDestroyed;

        protected override void Start()
        {
            base.Start();

            OnPlayerHit(_maxLives);
        }

        public override void Damage()
        {
            if (_remainingLives > 0)
            {
                _remainingLives--;
                OnPlayerHit(_remainingLives);
            }
            else
            {
                OnPlayerDestroyed();
                Destroy(this.gameObject);
            }
        }

        private void OnPlayerHit(int remainingLives)
        {
            onPlayerHit?.Invoke(remainingLives);
        }

        protected void OnPlayerDestroyed()
        {
            onPlayerDestroyed?.Invoke();
        }
    }
}
