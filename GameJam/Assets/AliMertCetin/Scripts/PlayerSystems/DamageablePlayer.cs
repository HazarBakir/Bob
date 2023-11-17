using System;
using UnityEngine;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class DamageablePlayer : MonoBehaviour, IDamageable
    {
        [SerializeField] Health health;

        void Awake() => health.Initialize();

        IDisposable IObservable<HealthChange>.Subscribe(IObserver<HealthChange> observer)
        {
            return health.Subscribe(observer);
        }

        bool IDamageable.CanReceiveDamage()
        {
            return health.isDepleted == false;
        }

        void IDamageable.ReceiveDamage(float amount)
        {
            health.DecreaseCurrentHealth(amount);
        }

        HealthData IDamageable.GetHealthData()
        {
            return health.GetHealthData();
        }
    }
}