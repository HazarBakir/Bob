using System.Collections;
using System.Collections.Generic;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class PlayerCollectObjectInteraction : InteractionHandlerBase
    {
        public List<string> items = new();
        
        public override IEnumerator OnInteractionStart(IInteractable interactable)
        {
            // Do nothing on interaction start
            yield break;
        }

        public override IEnumerator OnInteractionEnd(IInteractable interactable)
        {
            // Add item to inventory
            if (interactable is not CollectableObject collectableObject) yield break;
            items.Add(collectableObject.itemName);
            
            yield break;
        }
    }
}