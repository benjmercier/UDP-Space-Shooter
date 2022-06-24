using SpaceShooter.ExtensionMethods;
using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Powerups
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 3f;

        public static event Action onActivateTrippleShot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);

            if (transform.position.y < -10f)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                OnActivateTrippleShot();
                Destroy(this.gameObject);
            }

            //if (collision.transform.TryGetComponentInParents(out ICollidable collidable))
            //{
            //    if (collidable.Type == Category.Player)
            //    {
            //        OnActivateTrippleShot();
            //        Destroy(this.gameObject);
            //    }
            //}
        }

        private void OnActivateTrippleShot()
        {
            onActivateTrippleShot?.Invoke();
        }
    }
}
