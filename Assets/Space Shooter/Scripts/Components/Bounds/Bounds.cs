using SpaceShooter.ScriptableObjects;
using SpaceShooter.PropertyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class Bounds : MonoBehaviour
    {
        [SerializeField]
        protected BoundsSO _objBounds;

        protected Vector3 _currentPos;

        [ReadOnly]
        public bool applyBounds = true;

        // Update is called once per frame
        void Update()
        {
            if (applyBounds)
            {
                ApplyBounds();
            }            
        }

        protected abstract void ApplyBounds();
    }
}
