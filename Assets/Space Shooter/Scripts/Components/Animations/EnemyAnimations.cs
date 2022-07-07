using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components.Animations
{
    public class EnemyAnimations : MonoBehaviour, IAnimatable
    {
        [SerializeField]
        private Animator _animator;
        public Animator Animator => _animator;

        private static readonly int OnDestroyedID = Animator.StringToHash("OnDestroyed");

        private void Awake()
        {
            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
        }

        private void OnEnable()
        {
            _animator.ResetTrigger(OnDestroyedID);
        }

        public void PlayDestroyedAnim()
        {
            _animator.SetTrigger(OnDestroyedID);
        }
    }
}
