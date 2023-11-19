using System;
using System.Collections.Generic;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Rimaethon.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            EventManager.Instance.Broadcast(GameEvents.OnGameStart);
        }
    }
}