using UnityEngine;
using XIV.Packages.InventorySystem;
using XIV.Packages.InventorySystem.ScriptableObjects;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.InventorySystem
{
    /// <summary>
    /// Handles loading of the inventory. Informs other systems when inventory is loaded.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] InventorySO inventorySO;
        [SerializeField] InventoryChannelSO inventoryLoadedChannel;
        [SerializeField] VoidChannelSO onSceneReady;

        public Inventory inventory { get; private set; }

        void Awake() => inventory = inventorySO.GetInventory();
        void Start() => inventoryLoadedChannel.RaiseEvent(inventory);
        
        void OnEnable()
        {
            onSceneReady?.Register(OnSceneReady);
        }

        void OnDisable()
        {
            onSceneReady?.Unregister(OnSceneReady);
        }

        void OnSceneReady()
        {
            inventoryLoadedChannel.RaiseEvent(inventory);
        }
    }
}
