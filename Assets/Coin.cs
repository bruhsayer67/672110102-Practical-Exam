using Patawee_Hutamai;
using UnityEngine;

namespace aaa
{
    public class Coin : MonoBehaviour
    {
        [Header("Coin Settings")]
        public int pointValue = 10;

        [Header("Audio")]
        public AudioClip[] pickupSFX;
        [Range(0f, 1f)] public float sfxVolume = 1.0f;

        [Header("Animation")]
        public float rotationSpeed = 100f;
        public float floatAmplitude = 0.25f;
        public float floatFrequency = 2f;

        private Vector3 startPosition;

        // Tracks the last played sound index to prevent immediate repeats
        private static int lastPlayedIndex = -1;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            // Spin the coin continuously
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

            // Calculate vertical bobbing movement using a sine wave
            float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player picked up a coin");
                GameManager.AddScore(pointValue);

                if (pickupSFX != null && pickupSFX.Length > 0)
                {
                    int randomIndex = lastPlayedIndex;

                    // Pick a random clip different from the last one if we have multiple
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
                        // Calculate final volume safely inside the method
                        float globalSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
                        float finalVolume = sfxVolume * globalSFXVolume;

                        AudioSource.PlayClipAtPoint(selectedClip, transform.position, finalVolume);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}