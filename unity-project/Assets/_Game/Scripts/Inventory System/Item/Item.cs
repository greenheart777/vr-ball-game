using UnityEngine;

namespace _scripts
{
    public class Item : ScriptableObject
    {
        public string Id = "New ItemID";
        public string ItemName = "New Item Name";
        public ItemType Type;
        public GameObject Prefab;
        public int MaxStack = 999;
    }
}
