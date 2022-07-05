using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Points : MonoBehaviour, IScorable
    {
        [SerializeField]
        private int _pointsToAward;
        public int PointsToAward => _pointsToAward;
    }
}
