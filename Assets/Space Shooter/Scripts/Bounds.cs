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

    public class Bounds : MonoBehaviour
    {
        [SerializeField]
        private AxisConstraints _xAxis,
            _yAxis;

        private Vector3 _currentPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ApplyBounds();
        }

        private void ApplyBounds()
        {
            _currentPos = transform.position;

            /// if player pos is too far left/right (min/max)
            /// reset pos to opposite side
            if (transform.position.x < _xAxis.min)
            {
                _currentPos.x = _xAxis.max;
            }
            else if (transform.position.x > _xAxis.max)
            {
                _currentPos.x = _xAxis.min;
            }

            /// clamp player y pos to min/max value
            _currentPos.y = Mathf.Clamp(_currentPos.y, _yAxis.min, _yAxis.max);

            transform.position = _currentPos;
        }
    }
}
