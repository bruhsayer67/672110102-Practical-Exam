using Patawee_Hutamai;
using UnityEngine;

namespace Patawee_Hutamai
{
    public class Hazard : MonoBehaviour
    {
        [Header("Damage Settings")]
        public int damageAmount = 1;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the player touched the hazard
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }

        // Optional: If your hazard block is solid physics-based instead of a trigger
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}