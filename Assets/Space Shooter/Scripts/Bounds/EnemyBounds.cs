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
            if (transform.position.x < _objBounds.xAxis.min)
            {
                _currentPos.x = _objBounds.xAxis.max;
            }
            else if (transform.position.x > _objBounds.xAxis.max)
            {
                _currentPos.x = _objBounds.xAxis.min;
            }

            // if enemy pos < y min 
            // reset pos to random x between x min/max at y max
            if (transform.position.y < _objBounds.yAxis.min)
            {
                _currentPos.y = _objBounds.yAxis.max;

                _currentPos.x = Random.Range(_objBounds.xAxis.min, _objBounds.xAxis.max);
            }

            transform.position = _currentPos;
        }
    }
}
