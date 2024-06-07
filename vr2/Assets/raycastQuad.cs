using System.Xml;
using TMPro;
using UnityEngine;

public class RaycastChecker : MonoBehaviour
{
    // The tag of the quad to check against
    public string quadTag = "TargetQuad";
    private int attentionCount = 1;
    private int inAttentionCount = 1;
    private GameObject displayGO;
    private TextMeshPro displayText;

    void Start()
    {
        // Find the TextMeshPro component in the scene
        displayGO = GameObject.FindGameObjectWithTag("displayText");
        displayText = displayGO.GetComponent<TextMeshPro>();

    }


    void Update()
    {
        // Define the origin of the ray and its direction
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            // Check if the hit object has the specific tag
            if (hit.collider.CompareTag(quadTag))
            {
                // Debug.Log("Raycast hit the specific quad!");
                attentionCount += 1;
                
            }
            else
            {
                inAttentionCount += 1;
            }
        }
        else
        {
            // Increment inAttentionCount if the raycast does not hit anything
            inAttentionCount += 1;
        }

        // Ensure that both attentionCount and inAttentionCount are floats for the division
        float finalRatio = (float)attentionCount / (attentionCount + inAttentionCount);
        string ratioString = finalRatio.ToString("F2"); // Format the ratio to 2 decimal places
        displayText.text = ratioString;



    }
}
