using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems
{
    public static class AnimatorExtensions
    {
        public static float GetClipLength(this Animator animator, string clipName)
        {
            return GetClip(animator, clipName).length;
        }

        public static AnimationClip GetClip(this Animator animator, string clipName)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            int length = clips.Length;
            for (int i = 0; i < length; i++)
            {
                if (clips[i].name == clipName) return clips[i];
            }

            return default;
        }
    }
}