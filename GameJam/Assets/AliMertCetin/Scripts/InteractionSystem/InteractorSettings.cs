using System;
using UnityEngine;

namespace AliMertCetin.Scripts.InteractionSystem
{
    [Serializable]
    public struct InteractorSettings
    {
        public KeyCode interactionKey;
        /// <summary>
        /// Required distance for interaction
        /// </summary>
        public float distance;
    }
}