using Patawee_Hutamai;
using UnityEngine;

namespace Patawee_Hutamai
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
        private static int lastPlayedIndex = -1;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

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
                        // Get the saved SFX volume from PlayerPrefs
                        float globalSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
                        float finalVolume = sfxVolume * globalSFXVolume;

                        // Create a temporary GameObject to play the sound with the correct volume
                        GameObject audioObject = new GameObject("CoinAudio");
                        audioObject.transform.position = transform.position;
                        AudioSource source = audioObject.AddComponent<AudioSource>();
                        source.clip = selectedClip;
                        source.volume = finalVolume;
                        source.Play();

                        // Destroy the temporary audio object after the clip finishes playing
                        Destroy(audioObject, selectedClip.length);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}