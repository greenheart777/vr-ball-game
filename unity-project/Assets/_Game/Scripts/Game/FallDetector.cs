using UnityEngine;

namespace _scripts
{
    public class FallDetector : MonoBehaviour
    {
        public string targetTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                if (FallCounter.Instance != null)
                    FallCounter.Instance.RegisterFall();
            }
        }
    }
}