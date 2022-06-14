using SpaceShooter.ScriptableObjects;
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

        // Update is called once per frame
        void Update()
        {
            ApplyBounds();
        }

        protected abstract void ApplyBounds();

    }
}
