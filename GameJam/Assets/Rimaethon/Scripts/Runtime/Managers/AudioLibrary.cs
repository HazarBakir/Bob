using UnityEngine;

namespace Rimaethon.Runtime.Core
{
    [CreateAssetMenu(fileName="AudioLibrary" ,menuName = "Rimaethon/Audio Library",order = 1)]
    public class AudioLibrary:ScriptableObject
    {
        public AudioClip[] SFXClips;
        public AudioClip[] MusicClips;

    }
}