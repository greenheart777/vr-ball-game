using System;
using System.Collections.Generic;
using UnityEngine;

namespace _scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

        public IReadOnlyList<InventoryItem> Items => items;

        public event Action OnInventoryChanged;


        public bool AddItem(Item item, int count = 1)
        {
            foreach (var invItem in items)
            {
                if (invItem.Item == item && !invItem.IsFull)
                {
                    int space = item.MaxStack - invItem.Count;
                    int amountToAdd = Mathf.Min(space, count);

                    invItem.Count += amountToAdd;
                    count -= amountToAdd;

                    if (count <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }

            while (count > 0)
            {
                int amountToAdd = Mathf.Min(item.MaxStack, count);
                items.Add(new InventoryItem(item, amountToAdd));
                count -= amountToAdd;
            }

            OnInventoryChanged?.Invoke();
            return true;
        }

        public bool RemoveItem(Item item, int count = 1)
        {
            foreach (var invItem in items)
            {
                if (invItem.Item == item)
                {
                    if (invItem.Count >= count)
                    {
                        invItem.Count -= count;

                        if (invItem.Count == 0)
                            items.Remove(invItem);

                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasItem(Item item, int count = 1)
        {
            int total = 0;
            foreach (var invItem in items)
            {
                if (invItem.Item == item)
                    total += invItem.Count;
            }
            return total >= count;
        }

        public int GetItemCountById(string itemId)
        {
            int total = 0;
            foreach (var invItem in items)
            {
                if (invItem.Item != null && invItem.Item.Id == itemId)
                    total += invItem.Count;
            }
            return total;
        }
    }
}
