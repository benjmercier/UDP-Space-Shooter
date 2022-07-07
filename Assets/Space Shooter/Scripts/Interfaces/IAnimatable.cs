using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IAnimatable
    {
        Animator Animator { get; }
        void PlayDestroyedAnim();
    }
}
