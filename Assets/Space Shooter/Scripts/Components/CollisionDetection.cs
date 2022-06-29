using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionDetection : MonoBehaviour, ICollidable
    {
        [SerializeField]
        [Tooltip("Set to 'Default' for any item not the Player or an Enemy.")]
        private ObjType _objType;
        public ObjType Type { get => _objType; set => _objType = value; }
        
        private IDamageable _damageable;
        private IProtectable _protectable;

        private void Start()
        {
            if (TryGetComponent(out IDamageable damageable))
            {
                _damageable = damageable;
            }
            
            if (TryGetComponent(out IProtectable protectable))
            {
                _protectable = protectable;
                if (_protectable == null)
                {
                    Debug.Log("null");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponentInParents(out ICollidable otherCollidable))
            {
                if (otherCollidable.Type == _objType)
                {
                    return;
                }

                CollisionDetected();                            
            }
        }

        private void CollisionDetected()
        {
            if (_protectable != null && _protectable.ShieldActive)
            {
                _protectable.ShieldHit();

                return;
            }

            if (_damageable != null)
            {
                _damageable.Damage();
            }
        }

        

        // report collision on game object
        // - check if IDamageable component




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
