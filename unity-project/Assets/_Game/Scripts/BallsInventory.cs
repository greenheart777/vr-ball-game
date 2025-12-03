using TMPro;
using UnityEngine;

namespace _scripts
{
    public class BallsInventory : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory;
        [Space]
        [SerializeField] private Item ballItem;
        [SerializeField] private TMP_Text ballsCount;
        [SerializeField] private GameObject ballsSpawner;

        private void Awake()
        {
            if (playerInventory != null)
            {
                playerInventory.OnInventoryChanged += UpdateCount;
            }

            UpdateCount();
        }

        private void OnDestroy()
        {
            if (playerInventory != null)
            {
                playerInventory.OnInventoryChanged -= UpdateCount;
            }
        }

        private void UpdateCount()
        {
            var count = playerInventory.GetItemCountById(ballItem.Id);
            ballsCount.text = count.ToString();
            ballsSpawner.SetActive(count > 0);
        }
    } 
}
