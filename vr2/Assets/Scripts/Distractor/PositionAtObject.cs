using UnityEngine;

public class PositionAtObject : MonoBehaviour
{
    // The target object to get the world position from
    public GameObject targetObject;

    // Offsets for the x, y, and z positions
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;

    void Update()
    {
        if (targetObject != null)
        {
            // Get the world position of the target object
            Vector3 targetWorldPosition = targetObject.transform.position;

            // Apply the offsets
            targetWorldPosition.x += xOffset;
            targetWorldPosition.y += yOffset;
            targetWorldPosition.z += zOffset;

            // Set the position of the object this script is attached to
            transform.position = targetWorldPosition;
        }
    }
}
