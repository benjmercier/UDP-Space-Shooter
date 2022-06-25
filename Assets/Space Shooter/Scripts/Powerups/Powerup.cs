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
        // change to scriptable object
        [SerializeField]
        private int _powerupID;
        [SerializeField]
        private float _speed = 3f;

        public static event Action onActivateTrippleShot;
        public static event Action<float> onActivateSpeedBoost;

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
                switch(_powerupID)
                {
                    case 0:
                        OnActivateTrippleShot();
                        break;
                    case 1:
                        OnActivateSpeedBoost();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
                
                Destroy(this.gameObject);
            }
        }

        private void OnActivateTrippleShot()
        {
            onActivateTrippleShot?.Invoke();
        }

        private void OnActivateSpeedBoost()
        {
            onActivateSpeedBoost?.Invoke(1.5f);
        }
    }
}
