using UnityEngine;

namespace _scripts
{
    public class BonusTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject prizeDoor;

        private void Awake()
        {
            prizeDoor.SetActive(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            prizeDoor.SetActive(false);
        }
    } 
}
