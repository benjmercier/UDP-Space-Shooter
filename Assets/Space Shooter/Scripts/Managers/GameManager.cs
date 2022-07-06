using SpaceShooter.Components.Stats;
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

        private bool _gameOver = false;

        public static event Action<int> onUpdateScore;
        public static event Action onGameOver;

        private void Awake()
        {
            ResetPlayer();
        }

        private void OnEnable()
        {
            Health.onObjDestroyed += UpdateScore;
            PlayerHealth.onPlayerDestroyed += GameOver;
        }

        private void OnDisable()
        {
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
            _gameOver = true;

            OnGameOver();
        }

        private void OnGameOver()
        {
            onGameOver?.Invoke();
        }
    }
}