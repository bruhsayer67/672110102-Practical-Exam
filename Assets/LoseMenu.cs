using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Patawee_Hutamai
{
    public class LoseMenu : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Drag your TextMeshProUGUI score text element here")]
        public TextMeshProUGUI scoreText;

        void Start()
        {
            // Ensure cursor is visible and unlocked on the lose screen
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Retrieve and display the final score saved when time ran out or health hit zero
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
            if (scoreText != null)
            {
                scoreText.text = "Final Score: " + finalScore;
            }
        }

        public void RetryLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(2); // Replace with your actual gameplay scene name
        }

        public void ReturnToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0); // Replace with your actual main menu scene name
        }
    }
}