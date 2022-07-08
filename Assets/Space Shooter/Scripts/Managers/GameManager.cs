using SpaceShooter.Components.HealthStats;
using SpaceShooter.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private int _score;

        private bool _gameRunning = false;
        private bool _gameOver = false;

        public static event Action onStartGame;
        public static event Action<int> onUpdateScore;
        public static event Action onGameOver;

        private void Awake()
        {
            ResetPlayer();
        }

        private void OnEnable()
        {
            AsteroidHealth.onAsteroidDestroyed += StartGame;
            Health.onObjDestroyed += UpdateScore;
            PlayerHealth.onPlayerDestroyed += GameOver;            
        }

        private void OnDisable()
        {
            AsteroidHealth.onAsteroidDestroyed -= StartGame;
            Health.onObjDestroyed -= UpdateScore;
            PlayerHealth.onPlayerDestroyed -= GameOver;            
        }

        private void Update()
        {
            if (_gameOver && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void ResetPlayer()
        {
            _gameOver = false;
        }

        private void StartGame()
        {
            if (!_gameRunning)
            {
                _gameRunning = true;

                OnStartGame();
            }            
        }

        private void OnStartGame()
        {
            onStartGame?.Invoke();
        }

        private void UpdateScore(int points)
        {
            _score += points;

            OnUpdateScore(_score);
        }

        private void OnUpdateScore(int score)
        {
            onUpdateScore?.Invoke(score);
        }

        private void GameOver()
        {
            _gameRunning = false;
            _gameOver = true;

            OnGameOver();
        }

        private void OnGameOver()
        {
            onGameOver?.Invoke();
        }
    }
}
