using SpaceShooter.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.HealthStats
{
    public class PlayerHealth : Health
    {
        [SerializeField]
        private GameObject _damagePrefab;
        private GameObject _damageVisualizer;
        private List<GameObject> _activeDamage;
        [SerializeField]
        private Collider2D _collider2D;

        private float _randomX,
            _randomY;

        private Vector2 _randomV2;
        private Vector3 _randomV3;

        private UnityEngine.Bounds _colliderBounds;
        private Vector3 _boundsCenter;

        private int _randomPosAttempt;

        public static event Action<int> onPlayerHit;
        public static event Action onPlayerDestroyed;

        protected override void Start()
        {
            base.Start();

            if (_collider2D == null)
            {
                _collider2D = gameObject.GetComponentInChildren<Collider2D>();
            }

            OnPlayerHit(_maxLives);
        }

        public override void Damage()
        {
            if (_remainingLives > 0)
            {
                _remainingLives--;
                OnPlayerHit(_remainingLives);

                VisualizeDamage();
            }
            else
            {
                OnPlayerDestroyed();
                Destroy(this.gameObject);
            }
        }

        private void VisualizeDamage()
        {
            _damageVisualizer = Instantiate(_damagePrefab);
            _damageVisualizer.transform.parent = this.transform;
            _damageVisualizer.transform.position = SelectRandomDamagePosition();

            _activeDamage.Add(_damageVisualizer);
        }

        private Vector3 SelectRandomDamagePosition()
        {
            _colliderBounds = _collider2D.bounds;
            _boundsCenter = _colliderBounds.center;

            _randomX = 0;
            _randomY = 0;
            _randomV2 = Vector2.zero;

            _randomPosAttempt = 0;
            do
            {
                _randomX = UnityEngine.Random.Range(_boundsCenter.x - _colliderBounds.extents.x, _boundsCenter.x + _colliderBounds.extents.x);
                _randomY = UnityEngine.Random.Range(_boundsCenter.y - _colliderBounds.extents.y, _boundsCenter.y + _colliderBounds.extents.y);

                _randomV2 = new Vector2(_randomX, _randomY);

                _randomPosAttempt++;
            } while (!_collider2D.OverlapPoint(_randomV2) || _randomPosAttempt <= 100);

            _randomV3 = new Vector3(_randomX, _randomY, 0);

            return _randomV3;
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
