using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Laser : MonoBehaviour, IAttackable
    {
        [SerializeField]
        private float _speed = 5f;

        public Category Attacker { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            // change to pool
            Destroy(gameObject, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
    }
}
