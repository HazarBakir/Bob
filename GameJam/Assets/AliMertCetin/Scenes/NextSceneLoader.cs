using System;
using TheGame.SceneManagement;
using TheGame.ScriptableObjects.Channels;
using TheGame.ScriptableObjects.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using XIV.Core.Utils;

namespace AliMertCetin.Scenes
{
    public class NextSceneLoader : MonoBehaviour
    {
        [Header("Loads the next scene after timeline.duration + durationOffset")]
        [SerializeField] PlayableDirector timeline;
        [SerializeField] SceneListSO sceneListSO;
        [SerializeField] LevelDataChannelSO levelDataLoadedChannel;
        [SerializeField] SceneLoadChannelSO sceneLoadChannel;
        [SerializeField] float durationOffset = 2.5f;
        
        LevelData levelData;
        Timer timer;
        
        void Awake()
        {
            timer = new Timer((float)timeline.duration + durationOffset);
        }

        void OnEnable()
        {
            levelDataLoadedChannel.Register(OnLevelDataLoaded);
        }

        void OnLevelDataLoaded(LevelData obj)
        {
            levelData = obj;
        }

        void Update()
        {
            if (timer.Update(Time.deltaTime))
            {
                if (levelData != null && levelData.TryGetNextLevel(levelData.lastPlayedLevel, out var nextLevel))
                {
                    sceneLoadChannel.RaiseEvent(SceneLoadOptions.LevelLoad(nextLevel));
                }
                else
                {
                    sceneLoadChannel.RaiseEvent(SceneLoadOptions.MenuLoad(sceneListSO.mainMenuSceneIndex));
                }
            }
        }
    }
}
