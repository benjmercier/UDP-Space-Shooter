using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public enum Category
    {
        Player,
        Enemy
    }

    public interface IDamageable
    {
        Category ObjCategory { get; } 

        void Damage(Category category);
    }
}
