using System.Collections;
using UnityEngine;

namespace _scripts
{
    public class BallPickup : MonoBehaviour
    {
        [SerializeField] private Item ballItem;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;
        [Space]
        [SerializeField] private float destroyTime;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

        private Vector3 initialScale;

        private void Start()
        {
            initialScale = transform.localScale;
        }

        public void PickUp(Collider other)
        {
            StartCoroutine(MoveToPlayer(other.transform));
        }

        private IEnumerator MoveToPlayer(Transform target)
        {
            var rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            var targetHand = target.GetComponentInChildren<BallsSpawner>();
            if (targetHand != null)
            {
                var targetPos = targetHand.transform;
                if (targetPos != null)
                {
                    Vector3 startPos = transform.position;
                    float t = 0f;

                    while (t < 1f)
                    {
                        if (targetPos == null) break;

                        t += Time.deltaTime * moveSpeed;
                        float curvedT = moveCurve.Evaluate(t);
                        float scaleT = scaleCurve.Evaluate(t);

                        transform.position = Vector3.Lerp(startPos, targetPos.position, curvedT);
                        transform.localScale = initialScale * scaleT;

                        yield return null;
                    }

                    transform.localScale = Vector3.zero;
                    targetHand.PlayVFX();
                    PlayEndSFX();

                    var inv = target.GetComponentInChildren<Inventory>();
                    if (inv != null)
                    {
                        inv.AddItem(ballItem, 1);
                    }

                    yield return new WaitForSeconds(destroyTime);

                    Destroy(gameObject);
                }
            }
        }

        private void PlayEndSFX()
        {
            if (audioSource == null || audioClips == null || audioClips.Length == 0)
                return;

            var clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    }
}