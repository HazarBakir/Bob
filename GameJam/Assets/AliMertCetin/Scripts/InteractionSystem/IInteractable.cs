using UnityEngine;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public interface IInteractable : ISelectable
    {
        InteractionResult Interact<T>(T interactable) where T : Component, IInteractor;
        /// <summary>
        /// Return true if interaction is possible with <typeparamref name="T"/>
        /// </summary>
        /// <param name="interactable">The object who interacts with interactable</param>
        /// <returns>True if interaction is possible, false otherwise</returns>
        bool CanInteract<T>(T interactable) where T : Component, IInteractor;

        /// <summary>
        /// No need to implement this if it is <see cref="MonoBehaviour"/>
        /// </summary>
        T GetComponent<T>();
    }
}