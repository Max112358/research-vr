using UnityEngine;

public class ColliderTimer : MonoBehaviour
{
    private BoxCollider boxCollider; // Reference to the BoxCollider component on the same GameObject
    public float delay = 1.0f; // Delay in seconds before enabling the BoxCollider
    public float disableTime = 10.0f; // Time in seconds after which to disable the BoxCollider, starting from when it was enabled

    void Start()
    {
        // Get the BoxCollider component on the same GameObject
        boxCollider = GetComponent<BoxCollider>();

        // Check if the BoxCollider component is found
        if (boxCollider != null)
        {
            // Start the coroutine to enable and disable the BoxCollider
            StartCoroutine(EnableAndDisableCollider());
        }
        else
        {
            Debug.LogWarning("No BoxCollider component found on this GameObject.");
        }
    }

    private System.Collections.IEnumerator EnableAndDisableCollider()
    {
        // start off delayed
        boxCollider.enabled = false;


        // Enable BoxCollider after delay
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;

        // Wait for disableTime and then disable BoxCollider
        yield return new WaitForSeconds(disableTime);
        boxCollider.enabled = false;
    }
}
