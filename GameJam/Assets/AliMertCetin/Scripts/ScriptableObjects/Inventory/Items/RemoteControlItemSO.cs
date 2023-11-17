using System;
using System.Collections.Generic;
using UnityEngine;
using XIV.Packages.InventorySystem;
using XIV.Packages.InventorySystem.ScriptableObjects;

namespace AliMertCetin.Scripts.ScriptableObjects.Inventory.Items
{
    [Serializable]
    public class RemoteControlItem : ItemBase
    {
        public List<string> supportedChannels;
    }

    [CreateAssetMenu(menuName = MenuPaths.ITEMS_MENU + nameof(RemoteControlItem))]
    public class RemoteControlItemSO : ItemSO<RemoteControlItem>
    {
        
    }
}
