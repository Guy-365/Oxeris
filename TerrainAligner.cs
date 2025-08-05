using UnityEngine;

public class TerrainAlinger : MonoBehaviour
{
    public float alignSpeed = 5f;
    public float raycastDistance = 3f;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, groundLayer))
        {
            // Get the terrain's surface normal
            Vector3 normal = hit.normal;

            // Find the new rotation that matches the surface
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;

            // Smoothly rotate toward the terrain normal
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * alignSpeed);
        }
    }
}
