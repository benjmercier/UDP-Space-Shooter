using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoBehaviour, ICollidable
    {
        [SerializeField]
        private Category _objType;
        public Category Type { get => _objType; set => _objType = value; }

        [SerializeField]
        private Vector3 _startPos = new Vector3(0f, -3f, 0f);

        [SerializeField]
        private float _speed = 3.5f;

        private float _horInput,
            _vertInput;

        private Vector3 _inputDirection;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = _startPos;
        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            _horInput = Input.GetAxis("Horizontal");
            _vertInput = Input.GetAxis("Vertical");

            _inputDirection = new Vector3(_horInput, _vertInput, 0f);

            transform.Translate(_inputDirection * _speed * Time.deltaTime);
        }
    }
}
