using UnityEngine;
using TMPro;

namespace Patawee_Hutamai
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI scoreText;

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
    }
}