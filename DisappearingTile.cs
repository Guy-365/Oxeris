using UnityEngine;

public class DisappearingTile : MonoBehaviour
{
    public float reappearTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false); // Makes tile disappear
            Invoke(nameof(Reappear), reappearTime); // Bring it back after time
        }
    }

    void Reappear()
    {
        gameObject.SetActive(true); // Tile reappears
    }
}