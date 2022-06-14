using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.ScriptableObjects
{
    [System.Serializable]
    public struct AxisConstraints
    {
        public float min;
        public float max;
    }

    [CreateAssetMenu(fileName = "NewBounds.asset", menuName = "Scriptable Objects/Bounds")]
    public class BoundsSO : ScriptableObject
    {
        public AxisConstraints xAxis,
            yAxis;
    }
}
