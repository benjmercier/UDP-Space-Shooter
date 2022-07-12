using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IAudible
    {
        AudioSource AudioSource { get; }
        void PlayOneShot(AudioClip audioClip, float volumeScale = 1f);
    }
}
