using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip audioClipTest;
        [SerializeField] private AudioClip audioClipSelect;
        [SerializeField] private AudioClip audioClipDecide;
        [SerializeField] private AudioClip audioClipRotateTile;
        [SerializeField] private AudioClip audioClipPutTile;
        [SerializeField] private AudioClip audioClipConductLine;
        [SerializeField] private AudioClip[] audioClipsIlluminateLine;
        [SerializeField] private AudioClip audioClipIlluminateLineMax;
        [SerializeField] private AudioClip audioClipIlluminateBulb;
        [SerializeField] private AudioClip audioClipIlluminateTerminal;
        [SerializeField] private AudioClip audioClipIlluminateFrame;
        [SerializeField] private AudioClip audioClipShorted;
        [SerializeField] private AudioClip audioClipFatal;
        [SerializeField] private AudioClip audioClipBadElectric;
        
        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void ChangeVolume(float volume)
        {
            audioSource.volume = volume;
        }

        public void PlayTestSound()
        {
            audioSource.PlayOneShot(audioClipTest);
        }

        public void PlaySelectSound()
        {
            audioSource.PlayOneShot(audioClipSelect);
        }

        public void PlayDecideSound()
        {
            audioSource.PlayOneShot(audioClipDecide);
        }
        public void PlayRotateTileSound()
        {
            audioSource.PlayOneShot(audioClipRotateTile);
        }

        public void PlayPutTileSound()
        {
            audioSource.PlayOneShot(audioClipPutTile);
        }
        public void PlayConductLineSound()
        {
            audioSource.PlayOneShot(audioClipConductLine);
        }

        public void PlayIlluminateLineSound(int lineCount)
        {
            if (lineCount < audioClipsIlluminateLine.Length)
            {
                audioSource.PlayOneShot(audioClipsIlluminateLine[lineCount]);
            }
            else
            {
                audioSource.PlayOneShot(audioClipIlluminateLineMax);
            }
        }

        public void PlayIlluminateBulbSound()
        {
            audioSource.PlayOneShot(audioClipIlluminateBulb);
        }

        public void PlayIlluminateTerminalSound()
        {
            audioSource.PlayOneShot(audioClipIlluminateTerminal);
        }

        public void PlayIlluminateFrameSound()
        {
            audioSource.PlayOneShot(audioClipIlluminateFrame);
        }

        public void PlayShortedSound()
        {
            audioSource.PlayOneShot(audioClipShorted);
        }

        public void PlayFatalSound()
        {
            audioSource.PlayOneShot(audioClipFatal);
        }

        public void PlayBadElectricSound()
        {
            audioSource.PlayOneShot(audioClipBadElectric);
        }
    }
}
