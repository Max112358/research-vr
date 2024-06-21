using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class eyePathTracker : MonoBehaviour
{
    Transform transformOfObjectThisIsAttachedTo;


    // Specify the dimensions of your panoramic image
    public int imageWidth = 3840;
    public int imageHeight = 960;
    public int XOffset = 0;
    public int YOffset = 0;

    public string filePathStart = "Assets/GeneratedPath/";
    private string filePath;

    // Size of the dot (radius)
    private int dotSize = 4;

    private int arraySize = 300;
    private Vector2[] vectorArray;
    private int loopCount = 0;



    // Start is called before the first frame update
    void Start()
    {

        vectorArray = new Vector2[arraySize];

        // Assuming you have a reference to the camera transform
        transformOfObjectThisIsAttachedTo = GetComponent<Transform>();


        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = "eye_path_" + dateTime + ".txt";
        filePath = (filePathStart + fileName);
        Debug.Log("filePath: " + filePath);

        //GenerateAndSaveImage(imageWidth, imageHeight, dotX, dotY, dotSize, folderPath);
    }

    // Update is called once per frame
    void Update()
    {

        // Get the camera's rotation angles
        float rotationX = transformOfObjectThisIsAttachedTo.eulerAngles.y; // Yaw angle
        float rotationY = transformOfObjectThisIsAttachedTo.eulerAngles.x; // Pitch angle


        // Map the rotation angles to image coordinates
        Vector2 imageCoordinates = MapRotationToImageCoordinates(rotationX, rotationY, imageWidth, imageHeight);

        // Access the mapped coordinates
        float mappedX = imageCoordinates.x;
        float mappedY = imageCoordinates.y;
        //Debug.Log("x: " + mappedX + " y: " + mappedY);

        vectorArray[loopCount] = imageCoordinates;
        loopCount++;
        if (loopCount > arraySize - 1)
        {
            loopCount = 0;
            //drawPath(); too laggy
            exportPath();
        }

    }


    private void exportPath()
    {
        // Create a StreamWriter to write text to a file
        //StreamWriter writer = new StreamWriter(filePath);

        bool appendToFile = true;
        StreamWriter writer = new StreamWriter(filePath, appendToFile);

        for (int i = 0; i < vectorArray.Length; i++)
        {
            Vector2 currentVector = vectorArray[i];

            //GenerateAndSaveImage(imageWidth, imageHeight, (int)vectorArray[i].x, (int)vectorArray[i].y, dotSize, folderPath);
            //Debug.Log("Element " + i + ": (" + currentVector.x + ", " + currentVector.y + ")");\
            writer.WriteLine(currentVector.x + " " + currentVector.y);

        }

        writer.Close();

        //Debug.Log("Vector2 array exported to: " + filePath);
    }



    public Vector2 MapRotationToImageCoordinates(float rotationX, float rotationY, int imageWidth, int imageHeight)
    {

        // Normalize rotation angles and handle wrapping
        float normalizedX = (rotationX % 360f + 360f) % 360f / 360f;
        float normalizedY = ((rotationY + 90f) % 180f + 180f) % 180f / 180f;



        // Map normalized angles to image coordinates
        float x = (normalizedX * imageWidth + XOffset) % imageWidth;
        float y = (normalizedY * imageHeight + YOffset) % imageHeight;

        return new Vector2(x, y);
    }

}
