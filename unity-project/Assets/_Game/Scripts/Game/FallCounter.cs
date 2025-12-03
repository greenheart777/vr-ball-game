using TMPro;
using UnityEngine;

namespace _scripts
{
    public class FallCounter : MonoBehaviour
    {
        public static FallCounter Instance;
        public TMP_Text[] counterTexts;
        public int fallenCount = 0;

        [Header("Effects")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            if (counterTexts.Length > 0)
            {
                foreach (var counterText in counterTexts)
                {
                    counterText.text = $"{fallenCount}";
                }
            }
        }

        public void RegisterFall()
        {
            fallenCount++;

            UpdateText();
            PlaySFX();
        }

        public void UpdateText()
        {
            if (counterTexts.Length > 0)
            {
                foreach (var counterText in counterTexts)
                {
                    counterText.text = $"{fallenCount}";
                }
            }
        }


        private void PlaySFX()
        {
            if (audioSource == null || audioClips == null || audioClips.Length == 0)
                return;

            var clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    } 
}
