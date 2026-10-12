using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Patawee_Hutamai
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Drag your pause menu panel GameObject here")]
        public GameObject pauseMenuPanel;

        [Header("Audio Sliders")]
        public Slider bgmSlider;
        public Slider sfxSlider;

        [Header("Scene Settings")]
        public string mainMenuSceneName = "MainMenuScene"; // Make sure this matches your main menu scene name

        private bool isPaused = false;

        void Start()
        {
            ResumeGame();
        }

        void OnEnable()
        {
            // Match slider positions to saved preferences whenever the pause menu opens
            if (bgmSlider != null)
            {
                bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            isPaused = false;

            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(false);
            }

            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void PauseGame()
        {
            isPaused = true;

            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true);
            }

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // --- Audio Control Methods ---

        public void OnBGMVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("BGMVolume", value);
            PlayerPrefs.Save();

            // If you have a persistent BGM manager script, call its volume method here:
             PersistentBGM.SetVolume(value);
        }

        public void OnSFXVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            PlayerPrefs.Save();
        }

        // --- Navigation Methods ---

        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}