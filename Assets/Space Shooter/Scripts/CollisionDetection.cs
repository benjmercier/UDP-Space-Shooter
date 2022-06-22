using SpaceShooter.ExtensionMethods;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health), typeof(Rigidbody2D))]
    public class CollisionDetection : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponentInParents(out IDamageable damageable))
            {
                this.TryGetComponent(out Health health);
                damageable.Damage(health.ObjCategory);
            }
        }

        // if coming from obj,
        // then don't damage that obj


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
