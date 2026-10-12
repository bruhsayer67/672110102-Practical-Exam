using Patawee_Hutamai;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patawee_Hutamai
{
    public class Goal : MonoBehaviour
    {
        [Header("Scene Settings")]
        public string winSceneName = "WinScene";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Access the score via the new public property
                int finalScore = GameManager.CurrentScore;

                PlayerPrefs.SetInt("FinalScore", finalScore);
                PlayerPrefs.Save();

                Debug.Log("Player reached the goal! Final Score: " + finalScore);

                SceneManager.LoadScene(4);
            }
        }
    }
}