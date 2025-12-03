
namespace _scripts
{
    [System.Serializable]
    public class InventoryItem
    {
        public Item Item;
        public int Count;

        public InventoryItem(Item item, int count = 1)
        {
            this.Item = item;
            this.Count = count;
        }

        public bool IsFull => Count >= Item.MaxStack;
    }
}
