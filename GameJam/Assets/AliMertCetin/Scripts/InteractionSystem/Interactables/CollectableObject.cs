using UnityEngine;
using XIV.Core.TweenSystem;
using XIV.Core.Utils;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class CollectableObject : MonoBehaviour, IInteractable
    {
        public string itemName;
        bool isInInteraction;

        bool IInteractable.IsInInteraction => isInInteraction;

        InteractionSettings IInteractable.GetInteractionSettings()
        {
            return new InteractionSettings()
            {
                disableInteractionKey = false,
                suspendMovement = false,
            };
        }

        bool IInteractable.IsAvailableForInteraction()
        {
            return isInInteraction == false;
        }

        void IInteractable.Interact<T>(T interactor)
        {
            transform.CancelTween();
            transform.XIVTween()
                .Scale(Vector3.one, Vector3.zero, 0.5f, EasingFunction.EaseOutExpo)
                .OnComplete(() =>
                {
                    Destroy(this.gameObject);
                })
                .Start();
            interactor.OnInteractionEnd(this);
        }

        string IInteractable.GetInteractionString()
        {
            return "Collect!";
        }

        InteractionPositionData IInteractable.GetInteractionPositionData<T>(T interactor)
        {
            return new InteractionPositionData
            {
                startPos = interactor.transform.position,
                targetForwardDirection = transform.forward,
                targetPosition = transform.position,
            };
        }

        void ISelectable.Select()
        {
            PlaySelectionTween();
        }

        void ISelectable.Deselect()
        {
            PlaySelectionTween();
        }

        void PlaySelectionTween()
        {
            transform.CancelTween();
            transform.XIVTween()
                .Scale(Vector3.one, Vector3.one * 0.8f, 0.25f, EasingFunction.SmoothStart3)
                .Start();
        }
    }
}