using AliMertCetin.Scripts.PlayerSystems.FSM;
using UnityEngine;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.PlayerSystems
{
    [RequireComponent(typeof(PlayerFSM))]
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] TransformChannelSO playerLaodedChannel;
        [SerializeField] VoidChannelSO sceneLoadedChannel;

        void Start() => playerLaodedChannel.RaiseEvent(transform);
        void OnEnable() => sceneLoadedChannel.Register(OnSceneLoaded);
        void OnDisable() => sceneLoadedChannel.Unregister(OnSceneLoaded);
        void OnSceneLoaded() => playerLaodedChannel.RaiseEvent(transform);
    }
}