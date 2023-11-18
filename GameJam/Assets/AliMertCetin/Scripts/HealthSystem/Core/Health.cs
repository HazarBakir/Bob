using System;
using System.Collections.Generic;
using UnityEngine;

namespace XIV.DesignPatterns.Common.HealthSystem
{
    [System.Serializable]
    public class Health : IObservable<HealthChange>
    {
        public bool isDepleted => currentHealth < Mathf.Epsilon;
        public float normalized => currentHealth / maxHealth;
        public float max => maxHealth;
        public float current => currentHealth;

        [SerializeField] float maxHealth = 100f;
        [SerializeField] float currentHealth = 100f;
        
        List<IObserver<HealthChange>> _listeners;

        List<IObserver<HealthChange>> listeners => _listeners ??= new();

        public Health(float maxHealth, float currentHealth)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
        }

        public Health(float maxHealth) : this(maxHealth, maxHealth) { }
        
        public void IncreaseMaxHealth(float amount) => ChangeValue(ref maxHealth, amount, float.MaxValue);
        public void DecreaseMaxHealth(float amount) => ChangeValue(ref maxHealth, -amount, float.MaxValue);
        public void IncreaseCurrentHealth(float amount) => ChangeValue(ref currentHealth, amount, maxHealth);
        public void DecreaseCurrentHealth(float amount) => ChangeValue(ref currentHealth, -amount, maxHealth);

        void ChangeValue(ref float current, float amount, float max)
        {
            var newValue = Mathf.Clamp(current + amount, 0f, max);
            var diff = Mathf.Abs(newValue - current);
            HealthData healthDataBefore = GetHealthData();
            current = newValue;
            if (diff > 0f)
            {
                HealthData healthDataAfter = GetHealthData();
                var healthChange = new HealthChange(healthDataBefore, healthDataAfter);
                InformListenersOnHealthChange(healthChange);
            }
        }

        public HealthData GetHealthData() => new HealthData(maxHealth, currentHealth, isDepleted);

        void InformListenersOnHealthChange(HealthChange healthChange)
        {
            int count = listeners.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                listeners[i].OnNext(healthChange);
            }
        }

        // IObservable implementation
        public IDisposable Subscribe(IObserver<HealthChange> observer)
        {
            if (listeners.Contains(observer) == false)
            {
                listeners.Add(observer);
            }
            return new UnsubscribeContract<HealthChange>(observer, listeners);
        }
    }
}