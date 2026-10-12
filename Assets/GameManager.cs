using UnityEngine;
using TMPro;

namespace Patawee_Hutamai
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText; // New: Reference for health UI

        private int currentScore = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        void Start()
        {
            UpdateScoreUI();
        }

        // Call this to add score from anywhere
        public static void AddScore(int amount)
        {
            if (Instance != null)
            {
                Instance.currentScore += amount;
                Instance.UpdateScoreUI();
            }
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + currentScore;
            }
        }

        // New: Public method to update health text from the PlayerHealth script
        public static void UpdateHealthUI(int currentHealth, int maxHealth)
        {
            if (Instance != null && Instance.healthText != null)
            {
                Instance.healthText.text = "Health: " + currentHealth + " / " + maxHealth;
            }
        }

        // Public property to safely access the current score from other scripts
        public static int CurrentScore => Instance != null ? Instance.currentScore : 0;
    }
}