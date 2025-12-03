using UnityEngine;

namespace _scripts
{
    public class BallTrigger : MonoBehaviour
    {
        [SerializeField] private BallPickup ballPickup;
        [SerializeField] private string playerTag;

        private bool isPickedUp = false;

        private void OnTriggerEnter(Collider other)
        {
            if (isPickedUp) return;

            if (other.CompareTag(playerTag))
            {
                isPickedUp = true;

                var collider = GetComponent<Collider>();
                if (collider != null)
                    collider.enabled = false;

                ballPickup.PickUp(other);
            }
        }
    } 
}
