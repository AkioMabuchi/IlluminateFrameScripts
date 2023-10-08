using System;
using System.Collections;
using UnityEngine;

namespace Views
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSources = new AudioSource[3];
        
        [SerializeField] private float musicLength;

        private IEnumerator Start()
        {
            for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
            {
                yield return new WaitUntil(() => audioSources[0].time > musicLength);
                foreach (var audioSource in audioSources)
                {
                    audioSource.time = 0.0f;
                }
            }
        }

        public void ChangeVolume(float volume)
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }

        public void PlayMusic()
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.Play();
            }
        }

        public void MuteMain(bool isMute)
        {
            audioSources[1].mute = isMute;
        }

        public void MuteIlluminated(bool isMute)
        {
            audioSources[2].mute = isMute;
        }
    }
}
