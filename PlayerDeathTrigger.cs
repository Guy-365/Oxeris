using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour
{
    public Transform respawnPoint;
    public float damageAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit death zone!");

            // Optional: Damage the player
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            // Respawn player
            other.transform.position = respawnPoint.position;

            // Reset Rigidbody velocity if needed
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
               // rb.linearVelocity = Vector3.zero;
               // rb.angularVelocity = Vector3.zero;
            }
        }
    }
}