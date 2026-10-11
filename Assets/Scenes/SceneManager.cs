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
            PersistentBGM.SetVolume(value);
        }    

        public void OnSFXVolumeChanged(float value)
        {
            // Save the SFX volume preference
            PlayerPrefs.SetFloat("SFXVolume", value);
            PlayerPrefs.Save();
            Debug.Log("SFX Volume Saved: " + value);
        }
        void OnEnable()
        {
            // Match the slider position to the saved volume whenever the options panel opens
            if (bgmSlider != null)
            {
                bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            }
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