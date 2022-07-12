using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.HealthStats
{
    public class EnemyHealth : Health
    {
        [SerializeField]
        private Collider2D _mainCollider2D;

        protected override void Start()
        {
            base.Start();

            if (_mainCollider2D == null)
            {
                _mainCollider2D = GetComponentInChildren<Collider2D>();
            }
        }

        public override void Damage()
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

                OnObjDestroyed(_points);
                StartCoroutine(DestroyedRoutine());
            }
        }

        private IEnumerator DestroyedRoutine()
        {
            if (_mainCollider2D != null)
            {
                _mainCollider2D.enabled = false;
            }                        

            if (TryGetComponent(out Bounds bounds))
            {
                bounds.applyBounds = false;
            }

            if (TryGetComponent(out IAudible audible))
            {
                audible.PlayOneShot(_onDestroyedAudioClip);
            }

            yield return new WaitForSeconds(3f);

            Destroy(this.gameObject);
        }
    }
}
