using Rimaethon.Runtime.Core;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{ 
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] AudioLibrary audioLibrary;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        
        
        
        private AudioClip[] _musicClips;  
        private AudioClip[] _sfxClips;
        private bool musicOn;
        private bool sfxOn;
        private int _currentMusicIndex;
        private int _currentSFXIndex;

        protected override void Awake()
        {
            base.Awake();
            _musicClips = audioLibrary.MusicClips;
            _sfxClips = audioLibrary.SFXClips;
            
        }

        private void Start()
        {

            PlayMusic(MusicClips.InGameFastMusic);
        }
        public void PlayMusic(MusicClips clipEnum)
        {
            if (musicOn) return;
            if (musicSource.isPlaying) musicSource.Stop();
            _currentMusicIndex = (int)clipEnum;
            musicSource.clip = _musicClips[_currentMusicIndex];
            musicSource.Play();
        }
  
        public void PlaySFX(SFXClips clipEnum)
        {
            if (sfxOn) return;
            _currentSFXIndex = (int)clipEnum;
            sfxSource.PlayOneShot(_sfxClips[_currentSFXIndex]);
        }

    }
}
