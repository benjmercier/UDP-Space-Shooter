using SpaceShooter.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface ICollidable
    {
        Category Type { get; set; }
    }
}
