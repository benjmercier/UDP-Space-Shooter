using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyLaser : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        // Start is called before the first frame update
        void Start()
        {
            // change to pool
            Destroy(transform.root.gameObject, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
}
