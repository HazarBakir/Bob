using System.Collections;
using AliMertCetin.Scripts.InventorySystem;
using XIV.Packages.InventorySystem;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class PlayerCollectObjectInteraction : InteractionHandlerBase
    {
        Inventory inventory;
        
        void Start()
        {
            inventory = transform.parent.GetComponentInChildren<InventoryManager>().inventory;
        }

        public override IEnumerator OnInteractionStart(IInteractable interactable)
        {
            // Do nothing on interaction start
            yield break;
        }

        public override IEnumerator OnInteractionEnd(IInteractable interactable)
        {
            // Add item to inventory
            if (interactable is not CollectableObject collectableObject) yield break;

            var itemData = collectableObject.itemData;
            inventory.Add(itemData.itemSO.GetItem(), itemData.quantity);
            yield break;
        }
    }
}