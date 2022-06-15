using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _speed = 5f;

        public GameObject DamagingObj => this.gameObject;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
}
