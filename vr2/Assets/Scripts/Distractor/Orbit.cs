using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform centerObject;  // The object we want to orbit around
    public float orbitSpeedX = 20f;  // Speed of orbit in degrees per second for X axis
    public float orbitSpeedY = 10f;  // Speed of orbit in degrees per second for Y axis
    public float orbitSpeedZ = 5f;   // Speed of orbit in degrees per second for Z axis
    public float orbitDistance = 2f; // Distance from centerObject to orbiting object

    private void Update()
    {
        // Ensure centerObject is assigned and exists
        if (centerObject != null)
        {
            // Calculate rotation angles based on orbitSpeeds and Time
            float angleX = orbitSpeedX * Time.deltaTime;
            float angleY = orbitSpeedY * Time.deltaTime;
            float angleZ = orbitSpeedZ * Time.deltaTime;

            // Get current position relative to centerObject
            Vector3 relativePosition = transform.position - centerObject.position;

            // Rotate around the centerObject's position on specified axes
            Quaternion rotationX = Quaternion.AngleAxis(angleX, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(angleY, Vector3.up);
            Quaternion rotationZ = Quaternion.AngleAxis(angleZ, Vector3.forward);

            // Apply rotations sequentially (X, Y, Z)
            relativePosition = rotationX * relativePosition;
            relativePosition = rotationY * relativePosition;
            relativePosition = rotationZ * relativePosition;

            // Update position considering orbit distance
            Vector3 orbitPosition = centerObject.position + relativePosition.normalized * orbitDistance;
            transform.position = orbitPosition;
        }
        else
        {
            Debug.LogWarning("Center object not assigned for orbiting.");
        }
    }
}
