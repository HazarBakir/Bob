using UnityEngine;
using XIV.Packages.InventorySystem;
using XIV.Packages.InventorySystem.ScriptableObjects;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.InventorySystem
{
    [CreateAssetMenu(menuName = MenuPaths.BASE_MENU + "Channels/" + nameof(InventoryChannelSO))]
    public class InventoryChannelSO : XIVChannelSO<Inventory>
    {
        
    }
}