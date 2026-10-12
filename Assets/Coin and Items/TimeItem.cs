using Patawee_Hutamai;
using UnityEngine;

namespace aaa
{
    public class TimeItem : MonoBehaviour
    {
        [Header("Item Settings")]
        [Tooltip("Positive adds time (good item), negative reduces time (bad item)")]
        public float timeChangeAmount = 5f;
        [Tooltip("Optional score change when collected")]
        public int pointValue = 100;

        [Header("Audio")]
        public AudioClip[] pickupSFX;
        [Range(0f, 1f)] public float sfxVolume = 1.0f;

        [Header("Animation")]
        public float rotationSpeed = 100f;
        public float floatAmplitude = 0.25f;
        public float floatFrequency = 2f;

        private Vector3 startPosition;
        private static int lastPlayedIndex = -1;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            // Bobbing and rotating animation
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

            float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player collected item! Time change: " + timeChangeAmount);

                // Modify the game timer and score
                GameManager.AddTime(timeChangeAmount);
                if (pointValue != 0)
                {
                    GameManager.AddScore(pointValue);
                }

                // Play randomized pickup audio
                PlayPickupSound();

                // Destroy the item object
                Destroy(gameObject);
            }
        }

        private void PlayPickupSound()
        {
            if (pickupSFX != null && pickupSFX.Length > 0)
            {
                int randomIndex = lastPlayedIndex;

                if (pickupSFX.Length > 1)
                {
                    while (randomIndex == lastPlayedIndex)
                    {
                        randomIndex = Random.Range(0, pickupSFX.Length);
                    }
                }
                else
                {
                    randomIndex = 0;
                }

                lastPlayedIndex = randomIndex;
                AudioClip selectedClip = pickupSFX[randomIndex];

                if (selectedClip != null)
                {
                    float globalSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
                    float finalVolume = sfxVolume * globalSFXVolume;

                    GameObject audioObject = new GameObject("ItemAudio");
                    audioObject.transform.position = transform.position;
                    AudioSource source = audioObject.AddComponent<AudioSource>();
                    source.clip = selectedClip;
                    source.volume = finalVolume;
                    source.Play();

                    Destroy(audioObject, selectedClip.length);
                }
            }
        }
    }
}