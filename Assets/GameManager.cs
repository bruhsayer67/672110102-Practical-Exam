using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Patawee_Hutamai
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI timerText; // New: Timer text UI element

        [Header("Level Settings")]
        [SerializeField] private float timeRemaining = 30f;
        [SerializeField] private int targetScore = 10000;
        [SerializeField] private string winSceneName = "WinScene";
        [SerializeField] private string loseSceneName = "LoseScene";

        private int currentScore = 0;
        private bool isTimerRunning = true;

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
            currentScore = 0;
            UpdateScoreUI();
            UpdateTimerUI();
        }

        void Update()
        {
            if (isTimerRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    UpdateTimerUI();

                    if (timeRemaining <= 0)
                    {
                        timeRemaining = 0;
                        isTimerRunning = false;
                        TimeUp();
                    }
                }
            }
        }

        // Call this to add score from anywhere
        public static void AddScore(int amount)
        {
            if (Instance != null && Instance.isTimerRunning)
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

        public static void UpdateHealthUI(int currentHealth, int maxHealth)
        {
            if (Instance != null && Instance.healthText != null)
            {
                Instance.healthText.text = "Health: " + currentHealth + " / " + maxHealth;
            }
        }

        private void UpdateTimerUI()
        {
            if (timerText != null)
            {
                int seconds = Mathf.CeilToInt(timeRemaining);
                timerText.text = "Time: " + seconds + "s";
            }
        }
        public static void AddTime(float amount)
        {
            if (Instance != null && Instance.isTimerRunning)
            {
                Instance.timeRemaining += amount;
                Instance.UpdateTimerUI();
            }
        }

        private void TimeUp()
        {
            // Save final score so the win/lose screen can display it if needed
            PlayerPrefs.SetInt("FinalScore", currentScore);
            PlayerPrefs.Save();

            if (currentScore >= targetScore)
            {
                Debug.Log("Time's up! Target score reached. Loading Win Scene.");
                SceneManager.LoadScene(4);
            }
            else
            {
                Debug.Log("Time's up! Target score not met. Loading Lose Scene.");
                SceneManager.LoadScene(5);
            }
        }

        // Public property to safely access the current score from other scripts
        public static int CurrentScore => Instance != null ? Instance.currentScore : 0;
    }
}