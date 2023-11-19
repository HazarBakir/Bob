using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Rimaethon.Runtime.UI.UIElements
{
    public class HUDDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        private int _scoreCount;
        
        private void OnEnable()
        {
            EventManager.Instance.AddHandler<int>(GameEvents.OnScoreChange, UpdateScoreDisplay);
            EventManager.Instance.AddHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }
        
        private void OnDisable()
        {
            if (EventManager.Instance == null)return;
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnScoreChange, UpdateScoreDisplay);
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }
        
        private void UpdateScoreDisplay(int scoreAmount)
        {
            _scoreCount += scoreAmount;
            scoreText.text = $"Score: {_scoreCount}";
        }
        
        private void UpdateHealthDisplay(int healthValue)
        {
            healthText.text = healthValue.ToString();
        }
        
    }
}