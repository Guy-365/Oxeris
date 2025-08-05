using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public float fallThreshold = -10f;
    public float fallDamage = 25f;
    private bool hasFallen = false;

    void Update()
    {
        if (!hasFallen && player.transform.position.y < fallThreshold)
        {
            hasFallen = true;

            // Apply fall damage
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(fallDamage);
            }

            // Optional: Respawn after a delay
            Invoke("RespawnPlayer", 0.5f); // Delay to simulate impact
        }
    }

    public void RespawnPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        Rigidbody rb = player.GetComponent<Rigidbody>();

        Vector3 safePosition = respawnPoint.position + Vector3.up * 1f;

        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = safePosition;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = safePosition;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        hasFallen = false; // Allow falling again after respawn
    }
    void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger entered by: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("Player hit the trap, applying damage.");
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(10f);
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found on player!");
        }
    }
}
}