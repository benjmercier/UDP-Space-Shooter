using SpaceShooter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.HealthStats
{
    public class AsteroidHealth : Health
    {
        [SerializeField]
        private GameObject _explosionAnimPrefab;
        private GameObject _explosion;
        [SerializeField]
        private float _explosionAnimOffset = 0.15f;

        [SerializeField]
        private Collider2D _mainCollider2D;
        [SerializeField]
        private SpriteRenderer _mainSpriteRenderer;

        public static event Action onAsteroidDestroyed;

        protected override void Start()
        {
            base.Start();

            if (_mainCollider2D == null)
            {
                _mainCollider2D = GetComponentInChildren<Collider2D>();
            }

            if (_mainSpriteRenderer == null)
            {
                _mainSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
                StartCoroutine(DestroyedRoutine());
            }
        }

        private IEnumerator DestroyedRoutine()
        {
            if (_mainCollider2D != null)
            {
                _mainCollider2D.enabled = false;
            }

            _explosion = Instantiate(_explosionAnimPrefab, transform.position, Quaternion.identity);
            _explosion.transform.parent = this.transform;


            if (TryGetComponent(out IAudible audible))
            {
                audible.PlayOneShot(_onDestroyedAudioClip);
            }

            yield return new WaitForSeconds(_explosionAnimOffset);

            if (_mainSpriteRenderer != null)
            {
                _mainSpriteRenderer.enabled = false;
            }

            yield return new WaitForSeconds(2.5f);

            OnAsteroidDestroyed();
            Destroy(this.gameObject);
        }

        private void OnAsteroidDestroyed()
        {
            onAsteroidDestroyed?.Invoke();
        }
    }
}
