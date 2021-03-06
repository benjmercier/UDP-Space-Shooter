using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerBounds : Bounds
    {
        protected override void ApplyBounds()
        {
            _currentPos = transform.position;

            /// if player pos is too far left/right (min/max)
            /// reset pos to opposite side
            if (transform.position.x < _objBounds.xAxis.min)
            {
                _currentPos.x = _objBounds.xAxis.max;
            }
            else if (transform.position.x > _objBounds.xAxis.max)
            {
                _currentPos.x = _objBounds.xAxis.min;
            }

            /// clamp player y pos to min/max value
            _currentPos.y = Mathf.Clamp(_currentPos.y, _objBounds.yAxis.min, _objBounds.yAxis.max);

            transform.position = _currentPos;
        }
    }
}
