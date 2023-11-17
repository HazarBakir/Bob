using System;
using UnityEngine;
using UnityEngine.UI;
using XIV.DesignPatterns.Common.HealthSystem;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.Temp
{
    public class SliderHealthListener : MonoBehaviour, IObserver<HealthChange>
    {
        [SerializeField] Slider slider;
        [SerializeField] TransformChannelSO playerLoadedChannel;
        IDisposable unregisterContract;
        IDamageable damageable;

        void OnEnable()
        {
            playerLoadedChannel.Register(OnPlayerLoaded);
            unregisterContract = damageable?.Subscribe(this);
            Refresh();
        }

        void OnDisable()
        {
            playerLoadedChannel.Unregister(OnPlayerLoaded);
            unregisterContract?.Dispose();
        }

        void OnPlayerLoaded(Transform obj)
        {
            unregisterContract?.Dispose();
            damageable = obj.GetComponent<IDamageable>();
            unregisterContract = damageable?.Subscribe(this);
            Refresh();
        }

        void Refresh()
        {
            if (damageable == default) return;
            slider.value = damageable.GetHealthData().normalized;
        }

        void IObserver<HealthChange>.OnCompleted() { }

        void IObserver<HealthChange>.OnError(Exception error) { }

        void IObserver<HealthChange>.OnNext(HealthChange healthChange)
        {
            slider.value = healthChange.healthDataAfter.normalized;
        }
    }
}
