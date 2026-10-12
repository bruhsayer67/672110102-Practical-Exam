using Patawee_Hutamai;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patawee_Hutamai
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        public int maxHealth = 3;
        private int currentHealth;

        [Header("Visual Flash Settings")]
        [Tooltip("Your glowing red material")]
        public Material flashRedMaterial;
        [Range(0.05f, 0.5f)] public float flashDuration = 0.15f;

        private Renderer[] playerRenderers;
        private Material[][] originalMaterials;

        [Header("Scene Settings")]
        public string loseSceneName = "LoseScene";

        void Start()
        {
            currentHealth = maxHealth;
            GameManager.UpdateHealthUI(currentHealth, maxHealth);

            // Automatically find all renderers on the player and its children
            playerRenderers = GetComponentsInChildren<Renderer>();

            // Store all original materials safely
            originalMaterials = new Material[playerRenderers.Length][];
            for (int i = 0; i < playerRenderers.Length; i++)
            {
                originalMaterials[i] = playerRenderers[i].materials;
            }
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            Debug.Log("Player took damage! Current health: " + currentHealth);

            GameManager.UpdateHealthUI(currentHealth, maxHealth);

            if (playerRenderers != null && flashRedMaterial != null)
            {
                StopAllCoroutines();
                StartCoroutine(FlashRedRoutine());
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private IEnumerator FlashRedRoutine()
        {
            // Apply the flash material to every part
            foreach (Renderer rend in playerRenderers)
            {
                if (rend != null)
                {
                    Material[] flashMats = new Material[rend.materials.Length];
                    for (int j = 0; j < flashMats.Length; j++)
                    {
                        flashMats[j] = flashRedMaterial;
                    }
                    rend.materials = flashMats;
                }
            }

            // Wait for the duration of the flash
            yield return new WaitForSeconds(flashDuration);

            // Restore every part's original materials
            for (int i = 0; i < playerRenderers.Length; i++)
            {
                if (playerRenderers[i] != null)
                {
                    playerRenderers[i].materials = originalMaterials[i];
                }
            }
        }

        private void Die()
        {
            Debug.Log("Player died! Saving score and loading lose scene.");

            // Save the final score so the LoseMenu can display it
            PlayerPrefs.SetInt("FinalScore", GameManager.CurrentScore);
            PlayerPrefs.Save();

            SceneManager.LoadScene(5);
        }
    }
}