using UnityEngine;

namespace Patawee_Hutamai
{
    public class PersistentBGM : MonoBehaviour
    {
        private static PersistentBGM instance;
        private AudioSource audioSource;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            // Load saved volume on startup (defaults to 1.0 if nothing is saved)
            float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            audioSource.volume = savedVolume;
        }

        // Public method called by your UI slider
        public static void SetVolume(float volume)
        {
            if (instance != null && instance.audioSource != null)
            {
                instance.audioSource.volume = volume;
                PlayerPrefs.SetFloat("BGMVolume", volume);
                PlayerPrefs.Save();
            }
        }
    }
}