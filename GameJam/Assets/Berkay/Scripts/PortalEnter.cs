using System.Collections;
using System.Collections.Generic;
using TheGame.SceneManagement;
using TheGame.ScriptableObjects.Channels;
using TheGame.ScriptableObjects.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEnter : MonoBehaviour
{

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    [SerializeField] SceneListSO sceneListSO;
    [SerializeField] LevelDataChannelSO levelDataLoadedChannel;
    [SerializeField] SceneLoadChannelSO sceneLoadChannel;
    [SerializeField] float durationOffset = 2.5f;

    LevelData levelData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            if (levelData != null && levelData.TryGetNextLevel(levelData.lastPlayedLevel, out var nextLevel))
            {
                sceneLoadChannel.RaiseEvent(SceneLoadOptions.MenuLoad(sceneListSO.mainMenuSceneIndex));
            }


        }
                
        

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldLoadAfterFade = true;
           // UIFade.instance.FadeToBlack();
        }
    }
    void OnLevelDataLoaded(LevelData obj)
    {
        levelData = obj;
    }

}
