using System;
using UnityEngine;
using XIV.Core.TweenSystem;
using XIV.Core.Utils;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        public event Action<TestInteractable> onInteractableSelected;
        public event Action<TestInteractable> onInteractableDeselected;

        InteractionResult IInteractable.Interact<T>(T interactable)
        {
            // CancelTween cancels any active tween by completing or not completing it.
            transform.CancelTween(true);
            transform.XIVTween()
                .Scale(Vector3.one, Vector3.zero, 0.25f, EasingFunction.SmoothStart6)
                .OnComplete(() =>
                {
                    Destroy(this.gameObject);
                })
                .Start();
            
            return new InteractionResult()
            {
                success = true
            };
        }

        bool IInteractable.CanInteract<T>(T interactable)
        {
            return interactable.TryGetComponent<FreeLookCam>(out _);
        }

        void ISelectable.Select()
        {
            onInteractableSelected?.Invoke(this);
            transform.CancelTween();
            transform.XIVTween()
                .Scale(Vector3.one, Vector3.one * 0.8f, 0.2f, EasingFunction.EaseOutExpo)
                .Start();
        }

        void ISelectable.Deselect()
        {
            onInteractableDeselected?.Invoke(this);
            transform.CancelTween();
            transform.XIVTween()
                .Scale(Vector3.one, Vector3.one * 0.8f, 0.4f, EasingFunction.EaseOutExpo)
                .Start();
        }
    }
}