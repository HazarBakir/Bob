using System;

namespace XIV.DesignPatterns.Common.HealthSystem
{
    public interface IDamageable : IObservable<HealthChange>
    {
        bool CanReceiveDamage();
        void ReceiveDamage(float amount);
        HealthData GetHealthData();
    }
}