using SpaceShooter.Helpers;
using SpaceShooter.Interfaces;
using SpaceShooter.Powerups;
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
        private float _currentSpeed;

        private float _horInput,
            _vertInput;

        private Vector3 _inputDirection;

        private Coroutine _adjustSpeedRoutine;

        private void OnEnable()
        {
            Powerup.onActivateSpeedBoost += AdjustSpeed;
        }

        private void OnDisable()
        {
            Powerup.onActivateSpeedBoost -= AdjustSpeed;
        }

        // Start is called before the first frame update
        void Start()
        {
            transform.position = _startPos;
            _currentSpeed = _speed;
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

            transform.Translate(_inputDirection * _currentSpeed * Time.deltaTime);
        }

        private void AdjustSpeed(float speedMultiplier)
        {
            if (_adjustSpeedRoutine != null)
            {
                StopCoroutine(_adjustSpeedRoutine);
            }

            _adjustSpeedRoutine = StartCoroutine(AdjustSpeedRoutine(speedMultiplier));
        }

        private IEnumerator AdjustSpeedRoutine(float speedMultiplier)
        {
            _currentSpeed = _speed * speedMultiplier;

            yield return new WaitForSeconds(5f);

            _currentSpeed = _speed;
        }
    }
}
