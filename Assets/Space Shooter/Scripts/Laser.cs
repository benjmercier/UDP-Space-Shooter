using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

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
