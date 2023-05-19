using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void ChangeVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
