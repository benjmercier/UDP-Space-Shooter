using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health), typeof(Rigidbody2D))]
    public class CollisionDetection : MonoBehaviour, ICollidable
    {
        public GameObject GameObject => this.gameObject;
        private Health _objHealth;
        private Category _objAttacking = Category.Default;

        private void Start()
        {
            if (TryGetComponent(out Health health))
            {
                _objHealth = health;
            }
            else
            {
                Debug.LogError($"{_objHealth.GetType()} component not applied to {this.transform.name}");
            }           
            
            /// Checks if this object has Attack script with IAttackable interface
            /// If so, sets _objAttacking to IAttackable.Attacker 
            if (TryGetComponent(out IAttackable attackable))
            {
                _objAttacking = attackable.Attacker;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponentInParents(out ICollidable collidable))
            {
                /// Checks if collision occured with ICollidable implementing IAttackable
                /// If so, checks if other IAttackable = _objAttacking (Category.Default by default)
                /// If so, return w/o calling CollisionDetected()
                if (collidable.GameObject.TryGetComponent(out IAttackable attackable))
                {
                    if (_objAttacking == attackable.Attacker)
                    {
                        return;
                    }
                }

                CollisionDetected();
            }
        }

        private void CollisionDetected()
        {
            _objHealth.Damage();
        }




        //if (other.transform.parent != null)
        //{
        //    if (other.transform.parent.TryGetComponent(out IDamageable parentDamageable))
        //    {
        //        Destroy(parentDamageable.DamagingObj);
        //        Destroy(this.gameObject);
        //    }
        //}
        //else if (other.TryGetComponent(out IDamageable damageable))
        //{
        //    Destroy(this.gameObject);
        //}


        //IDamageable damageable1 = null;
        //Transform checker = other.gameObject.transform;

        //while (damageable1 == null)
        //{
        //    if (checker.TryGetComponent(out damageable1))
        //    {
        //        Destroy(damageable1.DamagingObj);
        //        Destroy(this.gameObject);
        //        return;
        //    }

        //    if (checker.parent != null)
        //    {
        //        checker = checker.parent;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        // check if other has IDamageable
        // if so, Destroy()
        // if not, check for parent
        // if parent != null
        // check for IDamageable
    }
}
