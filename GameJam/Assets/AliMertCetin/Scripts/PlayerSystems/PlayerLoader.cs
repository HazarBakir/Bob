using UnityEngine;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.EnemyAI
{
    [RequireComponent(typeof(PlayerMove))]
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