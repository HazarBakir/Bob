using TheGame.SceneManagement;
using TheGame.ScriptableObjects.Channels;
using TheGame.ScriptableObjects.SceneManagement;
using TheGame.UISystems.Core;
using UnityEngine;
using UnityEngine.UI;
using XIV.Packages.ScriptableObjects.Channels;

namespace TheGame.UISystems
{
    public class PauseUI : GameUI
    {
        [SerializeField] BoolChannelSO showPauseUIChannel;
        [SerializeField] SceneLoadChannelSO sceneLoadChannel;
        [SerializeField] SceneListSO sceneListSO;
        
        [SerializeField] Button btn_Resume;
        [SerializeField] Button btn_MainMenu;
        [SerializeField] BoolChannelSO gamePausedChannel;
        
        float previousTimeScale;

        void OnEnable()
        {
            btn_Resume.onClick.AddListener(Resume);
            btn_MainMenu.onClick.AddListener(GoToMainMenu);
        }

        void OnDisable()
        {
            btn_Resume.onClick.RemoveListener(Resume);
            btn_MainMenu.onClick.RemoveListener(GoToMainMenu);
        }

        protected override void OnUIActivated()
        {
            GameInput.Get<GameInput.UI>().onPausePerformed += Resume;
            
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            gamePausedChannel.RaiseEvent(true);
        }

        protected override void OnUIDeactivated()
        {
            GameInput.Get<GameInput.UI>().onPausePerformed -= Resume;
            
            // https://docs.unity3d.com/ScriptReference/Time-timeScale.html WTF?!
            Time.timeScale = previousTimeScale;
            gamePausedChannel.RaiseEvent(false);
        }
        
        void GoToMainMenu()
        {
            OnUIDeactivated();
            sceneLoadChannel.RaiseEvent(SceneLoadOptions.MenuLoad(sceneListSO.mainMenuSceneIndex));
        }
        
        void Resume()
        {
            showPauseUIChannel.RaiseEvent(false);
        }
    }
}