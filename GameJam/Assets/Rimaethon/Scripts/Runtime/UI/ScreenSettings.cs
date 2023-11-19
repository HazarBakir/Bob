using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class ScreenSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullScreenToggle;
        private int _screenWidth;
        private int _screenHeight;
        private bool _isFullScreen;
        private int _frameRate;

        private const string ScreenWidthKey = "ScreenWidth";
        private const string ScreenHeightKey = "ScreenHeight";
        private const string FullScreenKey = "FullScreen";
        private const string FrameRateKey = "FrameRate";

        private void Awake()
        {
            InitializeResolutionDropdown();
            LoadResolution();
        }

        private void OnEnable()
        {
            fullScreenToggle.onValueChanged.AddListener(FullScreenToggle);
        }

        private void InitializeResolutionDropdown()
        {
            resolutionDropdown.ClearOptions();
            Resolution[] resolutions = Screen.resolutions;
            List<string> resolutionOptions = new List<string>();

            foreach (Resolution res in resolutions)
            {
                string option = res.width + "x" + res.height;
                if (!resolutionOptions.Contains(option))
                {
                    resolutionOptions.Add(option);
                }
            }

            resolutionDropdown.AddOptions(resolutionOptions);
        }

        public void ChangeResolution()
        {
            string selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;
            string[] resolution = selectedResolution.Split('x');
            _screenWidth = int.Parse(resolution[0]);
            _screenHeight = int.Parse(resolution[1]);

            Screen.SetResolution(_screenWidth, _screenHeight, _isFullScreen);
        }

        private void LoadResolution()
        {
            for (int i = 0; i < resolutionDropdown.options.Count; i++)
            {
                string option = resolutionDropdown.options[i].text;
                string[] resolution = option.Split('x');
                if (int.Parse(resolution[0]) == _screenWidth && int.Parse(resolution[1]) == _screenHeight)
                {
                    resolutionDropdown.value = i;
                    resolutionDropdown.RefreshShownValue();
                    ChangeResolution();
                    return;
                }
            }
        }

        private void FullScreenToggle(bool isFullScreen)
        {
            _isFullScreen = isFullScreen;
            Screen.fullScreen = _isFullScreen;
        }

        private void LoadDataFromPlayerPrefs()
        {
            _screenHeight = PlayerPrefs.GetInt(ScreenHeightKey, Screen.currentResolution.height);
            _screenWidth = PlayerPrefs.GetInt(ScreenWidthKey, Screen.currentResolution.width);
            _isFullScreen = PlayerPrefs.GetInt(FullScreenKey, Screen.fullScreen ? 1 : 0) == 1;
            _frameRate = PlayerPrefs.GetInt(FrameRateKey, 60);

            LoadResolution();
            FullScreenToggle(_isFullScreen);
        }

        private void SaveDataToPlayerPrefs()
        {
            PlayerPrefs.SetInt(ScreenWidthKey, _screenWidth);
            PlayerPrefs.SetInt(ScreenHeightKey, _screenHeight);
            PlayerPrefs.SetInt(FullScreenKey, _isFullScreen ? 1 : 0);
            PlayerPrefs.SetInt(FrameRateKey, _frameRate);
            PlayerPrefs.Save();
        }

        private void OnDisable()
        {
            SaveDataToPlayerPrefs();
            fullScreenToggle.onValueChanged.RemoveListener(FullScreenToggle);
        }
    }
}
