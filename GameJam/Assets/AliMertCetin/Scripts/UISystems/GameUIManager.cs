using TheGame.SceneManagement;
using TheGame.ScriptableObjects.Channels;
using TheGame.UISystems.Core;
using TheGame.UISystems.SceneLoading;
using UnityEngine;
using XIV.Packages.ScriptableObjects.Channels;

namespace TheGame.UISystems
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] SceneLoadChannelSO displayLoadingScreenChannel;
        [SerializeField] VoidChannelSO stopDisplayingLoadingScreenChannel;
        [SerializeField] BoolChannelSO showPauseUIChannel;

        void OnEnable()
        {
            GameInput.Enable();
            GameInput.Get<GameInput.UI>().Enable();
            displayLoadingScreenChannel.Register(OnDisplayLoadingScreen);
            stopDisplayingLoadingScreenChannel.Register(OnStopDisplayingLoadingScreen);
            showPauseUIChannel.Register(OnShowPauseUI);
            GameInput.Get<GameInput.UI>().onPausePerformed += OnPausePerformed;
        }

        void OnDisable()
        {
            GameInput.Disable();
            GameInput.Get<GameInput.UI>().Disable();
            displayLoadingScreenChannel.Unregister(OnDisplayLoadingScreen);
            stopDisplayingLoadingScreenChannel.Unregister(OnStopDisplayingLoadingScreen);
            showPauseUIChannel.Unregister(OnShowPauseUI);
            GameInput.Get<GameInput.UI>().onPausePerformed -= OnPausePerformed;
        }

        void OnPausePerformed()
        {
            var pauseUI = UISystem.GetUI<PauseUI>();
            if (pauseUI == null) return;
            showPauseUIChannel.RaiseEvent(!pauseUI.isActive);
        }

        void OnShowPauseUI(bool val)
        {
            // Pause UI raises an event to inform other systems that game is paused or not
            if (val)
            {
                UISystem.Show<PauseUI>();
            }
            else
            {
                UISystem.Hide<PauseUI>();
            }
        }

        void OnDisplayLoadingScreen(SceneLoadOptions sceneLoadOptions)
        {
            var sceneLoadingUI = UISystem.GetUI<SceneLoadingUI>();
            sceneLoadingUI.SetSceneLoadingOptions(sceneLoadOptions);
            sceneLoadingUI.Show();
        }

        void OnStopDisplayingLoadingScreen()
        {
            UISystem.Hide<SceneLoadingUI>();
        }
    }
}