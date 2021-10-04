using UnityEngine;

namespace Assets.Scripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Current { get; private set; }

        private AudioSource _musicAudioSource;
        private float _volume = 0.5f;

        private void Awake()
        {
            Current = this;

            _musicAudioSource = GetComponent<AudioSource>();
            _volume = _musicAudioSource.volume;
        }

        public int GetVolumeLevel()
        {
            return Mathf.RoundToInt(_volume * 10);
        }

        public void SetVolume(float volume)
        {
            _volume = volume / 10;
            _volume = Mathf.Clamp01(_volume);

            _musicAudioSource.volume = _volume;
        }
    }
}