using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [System.Serializable]
    public class AxisConstraints
    {
        public float min;
        public float max;
    }

    public abstract class Bounds : MonoBehaviour
    {
        [SerializeField]
        protected AxisConstraints _xAxis,
            _yAxis;

        protected Vector3 _currentPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ApplyBounds();
        }

        protected abstract void ApplyBounds();

    }
}
