using TheGame.ScriptableObjects;
using TheGame.ScriptableObjects.Channels;
using UnityEngine;
using XIV.Packages.ScriptableObjects;
using XIV.Packages.ScriptableObjects.Channels;
using XIV_Packages.PCSettingSystems.Core;

namespace XIV_Packages.PCSettingSystems.Extras.ScriptableObjects.Channels
{
    [CreateAssetMenu(menuName = MenuPaths.CHANNEL_BASE_MENU + nameof(XIVSettingChannelSO))]
    public class XIVSettingChannelSO : XIVChannelSO<XIVSettings>
    {

    }
}
