using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public interface IScorable
    {
        public int PointsToAward { get; }
    }
}
