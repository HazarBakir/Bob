using UnityEngine;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public interface IInteractable : ISelectable
    {
        bool IsInInteraction { get; }
        InteractionSettings GetInteractionSettings();
        bool IsAvailableForInteraction();
        void Interact<T>(T interactor) where T : Component, IInteractor;
        string GetInteractionString();
        InteractionPositionData GetInteractionPositionData<T>(T interactor) where T : Component, IInteractor;
    }

    public struct InteractionPositionData
    {
        public Vector3 startPos; // Start position of interactor
        public Vector3 targetPosition; // target position of interactable in order to be able to interact with the object
        public Vector3 targetForwardDirection;
    }

    [System.Serializable]
    public struct InteractionSettings
    {
        public bool disableInteractionKey;
        public bool suspendMovement;
    }

    public interface ISelectable
    {
        void Select();
        void Deselect();
    }
}
