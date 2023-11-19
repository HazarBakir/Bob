using TheGame.ScriptableObjects.AudioManagement;
using UnityEngine;

namespace AliMertCetin
{
    public class AudioPlayerMono : MonoBehaviour
    {
        [SerializeField] AudioPlayerSO audioPlayer;

        void Start() => audioPlayer.Play();
        void OnDestroy() => audioPlayer.Stop();
    }
}
