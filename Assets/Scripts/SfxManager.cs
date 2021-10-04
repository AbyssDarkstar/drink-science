using UnityEngine;

namespace Assets.Scripts
{
    public class SfxManager : MonoBehaviour
    {
        public static SfxManager Current { get; private set; }
    
        private AudioSource _sfxAudioSource;
        private float _volume = 0.5f;

        private void Awake()
        {
            Current = this;
        
            _sfxAudioSource = GetComponent<AudioSource>();
        }

        public int GetVolumeLevel()
        {
            return Mathf.RoundToInt(_volume * 10);
        }

        public void SetVolume(float volume)
        {
            _volume = volume / 10;
            _volume = Mathf.Clamp01(_volume);

            _sfxAudioSource.volume = _volume;
        }

        public void PlayClip()
        {
            _sfxAudioSource.Play();
        }

        public void StopClip()
        {
            _sfxAudioSource.Stop();
        }
    }
}
