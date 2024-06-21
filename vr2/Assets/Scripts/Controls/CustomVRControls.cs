using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomVRControls : MonoBehaviour
{
    public GameObject leftControllerObject;
    public GameObject rightControllerObject;

    private XRController leftController;
    private XRController rightController;

    void Start()
    {
        // Find the XRController components on the specified GameObjects
        if (leftControllerObject != null)
        {
            leftController = leftControllerObject.GetComponent<XRController>();
        }

        if (rightControllerObject != null)
        {
            rightController = rightControllerObject.GetComponent<XRController>();
        }
    }

    void Update()
    {
        if (leftController != null)
        {
            if (leftController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
            {
                // Custom action for left trigger press
                Debug.Log("Left Trigger Pressed");
            }
        }

        if (rightController != null)
        {
            if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue)
            {
                // Custom action for right grip press
                Debug.Log("Right Grip Pressed");
            }
        }
    }
}
