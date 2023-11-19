using TheGame.ScriptableObjects.AudioManagement;
using UnityEngine;
using XIV.Packages.ScriptableObjects;
using XIV.Packages.ScriptableObjects.Channels;

namespace TheGame.ScriptableObjects.Channels
{
    [CreateAssetMenu(menuName = "Audio Management/" + nameof(AudioPlayOptionsChannelSO))]
    public class AudioPlayOptionsChannelSO : XIVChannelSO<AudioPlayOptions>
    {
        
    }
}