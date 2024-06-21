using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActions; // Assign the Input Actions asset here
    private InputAction turnAction;
    private InputAction resetAction;
    private CharacterController characterController;
    public float turnSpeed = 60f;
    private GameObject camera;
    public int cameraResetoffset = 90;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        var playerActionMap = inputActions.FindActionMap("XRI RightHand Locomotion");
        turnAction = playerActionMap.FindAction("Turn");
        resetAction = playerActionMap.FindAction("Reset");
    }

    void Start()
    {
        // Call the method to find the child with the "MainCamera" tag
        camera = FindChildWithTag(transform, "MainCamera");

        // Check if a child with the "MainCamera" tag was found
        if (camera != null)
        {
            //Debug.Log("MainCamera found: " + mainCamera.name);
        }
        else
        {
            Debug.Log("MainCamera not found");
        }
    }

    private void OnEnable()
    {
        turnAction.Enable();
    }

    private void OnDisable()
    {
        turnAction.Disable();
    }

    private void Update()
    {

        float cameraY = camera.transform.rotation.eulerAngles.y;


        bool buttonInput = resetAction.ReadValue<float>() > 0.5f;
        if (buttonInput)
        {
            //Debug.Log("Reset button pressed");
            // Perform the action for the button press here
            //Debug.Log(cameraY);
            float resetMath = 360 - cameraY + cameraResetoffset;
            transform.Rotate(0, resetMath, 0);
        }


        Vector2 stickValue = turnAction.ReadValue<Vector2>();
        //Debug.Log("Stick X: " + stickValue.x + " Stick Y: " + stickValue.y);
        float turn = stickValue.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);

    }

    // Method to recursively search for a child with a specific tag
    GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }

            GameObject found = FindChildWithTag(child, tag);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }
}
