using UnityEngine;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class TestPlayerInteraction : MonoBehaviour, IInteractor
    {
        [SerializeField] InteractorSettings interactorSettings = new InteractorSettings()
        {
            interactionKey = KeyCode.Z, // Other keys conflict with free look camera
            // Put default values here.
            distance = 2f
        };
        Camera cam;
        InteractionHandler interactionHandler;

        void Awake()
        {
            cam = Camera.main;
            interactionHandler = new InteractionHandler(cam, this);
        }

        void Update()
        {
            interactionHandler.Update();

            // No input, return
            if (Input.GetKeyDown(KeyCode.Z) == false) return;
            // There is no interactable selected
            if (interactionHandler.IsInteractableSelected() == false) return;
            
            IInteractable interactable = interactionHandler.GetCurrentInteractable();
            // Can't interact, return
            if (interactable.CanInteract(this) == false) return;
            InteractionResult result = interactable.Interact(this);
            
            // interaction is not successful, return
            if (result.success == false) return;
            Debug.LogWarning("You interacted with " + interactable.GetComponent<Transform>().gameObject.name);
        }
        
        InteractorSettings IInteractor.GetInteractorSettings() => interactorSettings;
    }
}