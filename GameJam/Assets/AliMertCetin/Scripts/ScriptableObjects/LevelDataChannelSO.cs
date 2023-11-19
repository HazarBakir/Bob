using TheGame.SceneManagement;
using UnityEngine;
using XIV.Packages.ScriptableObjects;
using XIV.Packages.ScriptableObjects.Channels;

namespace TheGame.ScriptableObjects.Channels
{
    [CreateAssetMenu(menuName = MenuPaths.CHANNEL_BASE_MENU + nameof(LevelDataChannelSO))]
    public class LevelDataChannelSO : XIVChannelSO<LevelData>
    {
        
    }
}