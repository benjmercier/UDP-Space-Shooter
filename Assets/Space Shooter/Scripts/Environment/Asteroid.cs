using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 3f;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}
