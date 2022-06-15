using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDetection : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent(out IDamageable parentDamageable))
                {
                    Destroy(parentDamageable.DamagingObj);
                    Destroy(this.gameObject);
                }
            }
            else if (other.TryGetComponent(out IDamageable damageable))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
