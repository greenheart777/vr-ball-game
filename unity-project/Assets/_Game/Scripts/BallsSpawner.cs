using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace _scripts
{
    public class BallsSpawner : MonoBehaviour
    {
        [SerializeField] private XRInteractionManager interactionManager;
        [SerializeField] private InputActionReference pickupInputAction;
        [SerializeField] private NearFarInteractor targetHandInteractor;
        [SerializeField] private Inventory playerInventory;

        [Header("Spawn Settings")]
        [SerializeField] private string targetTag = "Hand";
        [SerializeField] private Item ballItem;

        [Header("Vision Ball")]
        [SerializeField] private GameObject visionBall;
        [SerializeField] private float scaleDuration = 0.2f;
        [SerializeField] private Vector3 visionDefaultSize = Vector3.one;
        [SerializeField] private Vector3 visionHoverSize = Vector3.one;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;

        [Header("Punch Effect")]
        [SerializeField] private Vector3 punchScale = new Vector3(1.3f, 1.3f, 1.3f);
        [SerializeField] private float punchDuration = 0.15f;
        [SerializeField] private AnimationCurve punchCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private IXRSelectInteractor interactor;
        private bool isHovering;
        private Vector3 targetScale;
        private Vector3 currentVelocity;
        private bool isPunching;

        private void Awake()
        {
            interactor = targetHandInteractor;

            if (interactor == null)
                Debug.LogWarning("IXRInteractor not found");

            if (pickupInputAction != null)
                pickupInputAction.action.performed += OnSelectEntered;

            if (visionBall != null)
                visionBall.transform.localScale = visionDefaultSize;

            targetScale = visionDefaultSize;
        }

        private void OnDestroy()
        {
            if (pickupInputAction != null)
                pickupInputAction.action.performed -= OnSelectEntered;
        }

        private void Update()
        {
            if (visionBall != null && !isPunching)
            {
                visionBall.transform.localScale = Vector3.SmoothDamp(
                    visionBall.transform.localScale,
                    targetScale,
                    ref currentVelocity,
                    scaleDuration
                );
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                isHovering = true;
                SetVisionBallSize(visionHoverSize);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                isHovering = false;
                SetVisionBallSize(visionDefaultSize);
            }
        }

        private void OnSelectEntered(InputAction.CallbackContext context)
        {
            if (!isHovering || interactor == null) return;
            if (playerInventory.GetItemCountById(ballItem.Id) < 1) return;

            playerInventory.RemoveItem(ballItem, 1);

            var ball = Instantiate(ballItem.Prefab, transform.position, transform.rotation);
            var grab = ball.GetComponent<XRGrabInteractable>();

            if (grab != null)
                interactionManager.SelectEnter(interactor, grab);

            PlaySFX();
            PlayVFX();
        }



        public void PlaySFX()
        {
            if (audioSource == null || audioClips == null || audioClips.Length == 0)
                return;

            var clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }

        public void PlayVFX()
        {
            if (playerInventory.GetItemCountById(ballItem.Id) > 0)
                StartCoroutine(PunchEffect());
        }

        public IEnumerator PunchEffect()
        {
            if (visionBall == null) yield break;

            isPunching = true;
            Vector3 startScale = targetScale;
            float elapsed = 0f;

            while (elapsed < punchDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / punchDuration;
                float curveValue = punchCurve.Evaluate(t);

                Vector3 currentPunchScale = Vector3.Lerp(startScale, punchScale, 1f - Mathf.Abs(curveValue * 2f - 1f));
                visionBall.transform.localScale = currentPunchScale;

                yield return null;
            }

            visionBall.transform.localScale = startScale;
            isPunching = false;
        }

        private void SetVisionBallSize(Vector3 size)
        {
            targetScale = size;
        }

    }
}