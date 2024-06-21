using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLowPassBehind : MonoBehaviour
{
    public Transform transformOfMainCamera;
    private AudioLowPassFilter lowPassFilter;


    // Start is called before the first frame update
    void Start()
    {

        // Assuming you have a reference to the camera transform
        //transformOfMainCamera = GetComponent<Transform>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();

    }

    // Update is called once per frame
    void Update()
    {

        // Get the camera's rotation angles
        float rotationX = transformOfMainCamera.eulerAngles.y; // Yaw angle
        float rotationY = transformOfMainCamera.eulerAngles.x; // Pitch angle




        // Map the rotation angles to image coordinates
        //Vector2 imageCoordinates = MapRotationToImageCoordinates(rotationX, rotationY, imageWidth, imageHeight);

        // Access the mapped coordinates
        //float mappedX = imageCoordinates.x;
        //float mappedY = imageCoordinates.y;
        //Debug.Log("x: " + rotationX + " y: " + rotationY);
        //Debug.Log("x math: " + Mathf.Abs( 90 - (rotationX - 180) ));
        //Debug.Log("x math: " + (rotationX - 180));

        float minimumSound = 2000;
        float maximumSound = 22000;
        float backOfRoomAngle = rotationX - 180;

        if (backOfRoomAngle > 0)
        {
            //Debug.Log("x math: " + Mathf.Abs(backOfRoomAngle - 90));
            float normalizedRelativeMuffleStrength = (Mathf.Abs(backOfRoomAngle - 90) / 90);
            //Debug.Log("x normal: " + normalizedRelativeMuffleStrength);
            float cutoffFrequency = minimumSound + (normalizedRelativeMuffleStrength * (maximumSound - minimumSound));
            //Debug.Log("x final: " + finalNumber);
            lowPassFilter.cutoffFrequency = cutoffFrequency;
        }
        else
        {
            lowPassFilter.cutoffFrequency = maximumSound;
        }



    }

    public Vector2 MapRotationToImageCoordinates(float rotationX, float rotationY, int imageWidth, int imageHeight)
    {

        // Normalize rotation angles and handle wrapping
        float normalizedX = (rotationX % 360f + 360f) % 360f / 360f;
        float normalizedY = ((rotationY + 90f) % 180f + 180f) % 180f / 180f;



        // Map normalized angles to image coordinates
        float x = (normalizedX * imageWidth) % imageWidth;
        float y = (normalizedY * imageHeight) % imageHeight;

        return new Vector2(x, y);
    }
}
