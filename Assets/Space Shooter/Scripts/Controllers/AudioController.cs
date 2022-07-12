using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour, IAudible
    {
        [SerializeField]
        private AudioSource _audioSource;
        public AudioSource AudioSource => _audioSource;

        private AudioClip _activeAudioClip;

        private void Awake()
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }

        public void PlayOneShot(AudioClip audioClip, float volumeScale = 1f)
        {
            _audioSource.PlayOneShot(audioClip, volumeScale);
        }
    }
}
