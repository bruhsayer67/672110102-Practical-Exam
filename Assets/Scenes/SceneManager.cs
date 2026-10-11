using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Patawee_Hutamai
{
    public class Menu : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject optionsPanel;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;

        // --- Main Menu Functions ---

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game requested.");
            Application.Quit();
        }

        // --- Options Menu Functions ---

        public void OpenOptions()
        {
            if (optionsPanel != null)
                optionsPanel.SetActive(true);
        }

        public void CloseOptions()
        {
            if (optionsPanel != null)
                optionsPanel.SetActive(false);
        }

        public void OnBGMVolumeChanged(float value)
        {
            Debug.Log("BGM Volume set to: " + value);
            // Connect to your AudioSource or AudioMixer here
        }

        public void OnVFXVolumeChanged(float value)
        {
            Debug.Log("VFX Volume set to: " + value);
            // Connect to your AudioSource or AudioMixer here
        }

        // --- Stage Select Functions ---

        public void LoadStage1()
        {
            SceneManager.LoadScene(2);
        }

        public void LoadStage2()
        {
            SceneManager.LoadScene(3);
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}