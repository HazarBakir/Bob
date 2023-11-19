using System;
using TheGame.ScriptableObjects.Channels;
using UnityEngine;
using XIV.Core;

namespace TheGame.ScriptableObjects.AudioManagement
{
    [CreateAssetMenu(menuName = "Audio Management/" + nameof(AudioPlayerSO))]
    public class AudioPlayerSO : ScriptableObject
    {
        [SerializeField] AudioPlayOptionsChannelSO audioPlayOptionsChannel;
        [SerializeField] AudioClip clip;
        [SerializeField] AudioType audioType;

        [Button]
        public void Play()
        {
            audioPlayOptionsChannel.RaiseEvent(GetOption());
        }

        [Button]
        public void Stop()
        {
            audioPlayOptionsChannel.RaiseEvent(new AudioPlayOptions(audioType, null, false, null));
        }

        AudioPlayOptions GetOption()
        {
            return audioType switch
            {
                AudioType.None => default,
                AudioType.Music => AudioPlayOptions.MusicPlayOptions(clip),
                AudioType.Effect => AudioPlayOptions.EffectPlayOptions(clip),
                _ => throw new NotImplementedException(audioType + " is not implemented.")
            };
        }
    }
}