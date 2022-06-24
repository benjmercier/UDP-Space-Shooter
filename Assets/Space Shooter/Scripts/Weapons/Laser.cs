using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Laser : MonoBehaviour, ICollidable
    {
        [SerializeField]
        private float _speed = 5f;

        public Category Type { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            // change to pool
            Destroy(transform.root.gameObject, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
    }
}
