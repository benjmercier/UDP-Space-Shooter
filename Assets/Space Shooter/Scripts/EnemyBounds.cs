using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyBounds : Bounds
    {
        protected override void ApplyBounds()
        {
            _currentPos = transform.position;

            /// if pos is too far left/right (min/max)
            /// reset pos to opposite side
            if (transform.position.x < _xAxis.min)
            {
                _currentPos.x = _xAxis.max;
            }
            else if (transform.position.x > _xAxis.max)
            {
                _currentPos.x = _xAxis.min;
            }

            // if enemy pos < y min 
            // reset pos to random x between x min/max at y max
            if (transform.position.y < _yAxis.min)
            {
                _currentPos.y = _yAxis.max;

                _currentPos.x = Random.Range(_xAxis.min, _xAxis.max);
            }

            /// clamp player y pos to min/max value
            _currentPos.y = Mathf.Clamp(_currentPos.y, _yAxis.min, _yAxis.max);

            transform.position = _currentPos;
        }
    }
}
