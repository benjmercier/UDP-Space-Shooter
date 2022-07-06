using SpaceShooter.Components.Stats;
using SpaceShooter.Helpers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("Score")]
        [SerializeField]
        private TMP_Text _scoreCountTMP;

        [Header("Lives")]
        [SerializeField]
        private Image _livesImg;
        [SerializeField]
        private List<Sprite> _livesSprites;

        [Header("Restart")]
        [SerializeField]
        private TMP_Text _restartTMP;

        [Header("Game Over")]
        [SerializeField]
        private TMP_Text _gameOverTMP;
        private bool _runGameOverRoutine = false;

        [SerializeField]
        private float _textFlickerOn = 1f,
            _textFlickerOff = 0.5f;

        private WaitForSeconds _showTextTime;
        private WaitForSeconds _hideTextTime;

        private void OnEnable()
        {
            GameManager.onUpdateScore += UpdateScoreText;
            GameManager.onGameOver += ActivateGameOverTMP;

            PlayerHealth.onPlayerHit += UpdateLivesImage;
        }

        private void OnDisable()
        {
            GameManager.onUpdateScore -= UpdateScoreText;
            GameManager.onGameOver -= ActivateGameOverTMP;

            PlayerHealth.onPlayerHit -= UpdateLivesImage;
        }

        private void Start()
        {
            EnableTMP(_gameOverTMP, false);
            EnableTMP(_restartTMP, false);

            SetFlickerWaitForSeconds(_textFlickerOn, _textFlickerOff);
        }

        private void SetFlickerWaitForSeconds(float showTime, float hideTime)
        {
            _showTextTime = new WaitForSeconds(showTime);
            _hideTextTime = new WaitForSeconds(hideTime);
        }

        private void UpdateScoreText(int score)
        {
            _scoreCountTMP.text = score.ToString();
        }

        private void UpdateLivesImage(int lives)
        {
            _livesImg.sprite = _livesSprites[lives];
        }

        private void ActivateGameOverTMP()
        {            
            _runGameOverRoutine = true;

            EnableTMP(_gameOverTMP, _runGameOverRoutine);
            EnableTMP(_restartTMP, true);

            StartCoroutine(GameOverTMPFlickerRoutine());
        }

        private void EnableTMP(TMP_Text tmpText, bool enabled)
        {
            tmpText.enabled = enabled;
        }

        private IEnumerator GameOverTMPFlickerRoutine()
        {
            while (_runGameOverRoutine)
            {
                yield return _showTextTime;

                EnableTMP(_gameOverTMP, false);

                yield return _hideTextTime;

                EnableTMP(_gameOverTMP, true);
            }
        }
    }
}
