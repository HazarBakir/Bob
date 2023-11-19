using System;
using System.Collections;
using TheGame.SceneManagement;
using TheGame.UISystems.Core;
using UnityEngine;
using XIV.Packages.ScriptableObjects.Channels;

namespace TheGame.UISystems.SceneLoading
{
    public class SceneLoadingUI : GameUI
    {
        [SerializeField] FloatChannelSO sceneLoadingProgressChannel;
        [SerializeField] VoidChannelSO newSceneLoadedChannel;
        [SerializeField] VoidChannelSO activateNewlyLoadedScene;
        [SerializeField] BoolChannelSO activateLoadingScreenCamera;
        
        [SerializeField] LevelLoadingUI levelLoadingUI;
        [SerializeField] MenuLoadingUI menuLoadingUI;

        SceneLoadOptions sceneLoadOptions;
        LoadingUIBase currentLoadingDisplay;
        
        void OnEnable()
        {
            sceneLoadingProgressChannel.Register(UpdateProgressBar);
            newSceneLoadedChannel.Register(OnSceneLoaded);
        }

        void OnDisable()
        {
            sceneLoadingProgressChannel.Unregister(UpdateProgressBar);
            newSceneLoadedChannel.Unregister(OnSceneLoaded);
        }

        public override void Show()
        {
            isActive = true;
            uiGameObject.SetActive(true);
            OnUIActivated();
            activateLoadingScreenCamera.RaiseEvent(true);
        }

        protected override void OnUIActivated()
        {
            switch (sceneLoadOptions.loadingScreenType)
            {
                case LoadingScreenType.None:
                    return;
                case LoadingScreenType.LevelLoading:
                    currentLoadingDisplay = levelLoadingUI;
                    break;
                case LoadingScreenType.MenuLoading:
                    currentLoadingDisplay = menuLoadingUI;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            currentLoadingDisplay.Show();
            currentLoadingDisplay.onUIClosed += OnLoadingDisplayClosed;
        }

        void OnLoadingDisplayClosed(LoadingUIBase obj)
        {
            currentLoadingDisplay.onUIClosed -= OnLoadingDisplayClosed;
            uiGameObject.SetActive(false);
            isActive = false;
            currentLoadingDisplay = null;
            OnUIDeactivated();
        }

        public override void Hide()
        {
            if (currentLoadingDisplay == null)
            {
                uiGameObject.SetActive(false);
                isActive = false;
                OnUIDeactivated();
                return;
            }

            currentLoadingDisplay.Hide();
        }

        protected override void OnUIDeactivated()
        {
            activateLoadingScreenCamera.RaiseEvent(false);
        }

        public void SetSceneLoadingOptions(SceneLoadOptions sceneLoadOptions)
        {
            this.sceneLoadOptions = sceneLoadOptions;
        }

        void UpdateProgressBar(float value)
        {
            if (isActive == false || currentLoadingDisplay == false) return;
            currentLoadingDisplay.UpdateProgressBar(value);
        }

        void OnSceneLoaded()
        {
            if (sceneLoadOptions.activateImmediately) return;

            StartCoroutine(WaitForInput());
        }

        IEnumerator WaitForInput()
        {
            while (Input.anyKey)
            {
                yield return null;
            }
            activateNewlyLoadedScene.RaiseEvent();
        }
        
    }
}
