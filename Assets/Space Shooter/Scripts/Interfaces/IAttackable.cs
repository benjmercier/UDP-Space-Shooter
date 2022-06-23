using SpaceShooter.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public interface IAttackable
    {
        Category Attacker { get; set; }
    }
}
