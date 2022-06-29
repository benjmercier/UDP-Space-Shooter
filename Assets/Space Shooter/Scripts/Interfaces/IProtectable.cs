using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IProtectable
    {
        GameObject ShieldObj { get; }
        bool ShieldActive { get; }
        void ShieldHit();
    }
}
