using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SfxVolumeKey = "SFXVolume";

        private void Awake()
        {
            if (audioMixer == null || masterVolumeSlider == null || sfxVolumeSlider == null || musicVolumeSlider == null)
            {
                Debug.LogError("One or more components are not assigned in the Inspector.");
                enabled = false;
                return;
            }

            LoadVolumes();
        }

        private void OnEnable()
        {
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDisable()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void SetMasterVolume(float volume)
        {
            SetVolume(MasterVolumeKey, masterVolumeSlider, volume);
        }

        private void SetMusicVolume(float volume)
        {
            SetVolume(MusicVolumeKey, musicVolumeSlider, volume);
        }

        private void SetSfxVolume(float volume)
        {
            SetVolume(SfxVolumeKey, sfxVolumeSlider, volume);
        }

        private void SetVolume(string volumeTypeKey, Slider volumeSlider, float volume)
        {
            audioMixer.SetFloat(volumeTypeKey, Mathf.Lerp(-80, 0, volume));
            volumeSlider.value = volume;
            PlayerPrefs.SetFloat(volumeTypeKey, volume);
        }

        private void LoadVolumes()
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat(MasterVolumeKey, masterVolumeSlider.value);
            musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolumeSlider.value);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat(SfxVolumeKey, sfxVolumeSlider.value);
        }
    }
}
