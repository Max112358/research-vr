using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class eyePathTracker : MonoBehaviour
{
    Transform transformOfObjectThisIsAttachedTo;


    // Specify the dimensions of your panoramic image
    public int imageWidth = 3840;
    public int imageHeight = 960;
    public int XOffset = 0;
    public int YOffset = 0;

    // Specify the coordinates for the black dot
    public int dotX = 256;
    public int dotY = 256;

    public string folderPath = "Assets/GeneratedImages/";

    // Size of the dot (radius)
    public int dotSize = 4;

    public Vector2[] vectorArray;
    private int loopCount = 0;



    // Start is called before the first frame update
    void Start()
    {

        vectorArray = new Vector2[300];

        // Assuming you have a reference to the camera transform
        transformOfObjectThisIsAttachedTo = GetComponent<Transform>();

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
        if (loopCount > 299)
        {
            loopCount = 0;

        }

    }

    private void drawPath()
    {
        for (int i = 0; i < vector2Array.Length; i++)
        {
            Vector2 currentVector = vector2Array[i];
            Debug.Log("Element " + i + ": (" + currentVector.x + ", " + currentVector.y + ")");
        }
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

    void GenerateAndSaveImage(int width, int height, int dotX, int dotY, int dotRadius, string folderPath)
    {

        dotY = height - dotY;


        // Create a new texture with the specified width and height
        Texture2D texture = new Texture2D(width, height);

        // Fill the texture with a transparent color
        Color32[] pixels = texture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }
        texture.SetPixels32(pixels);

        // Draw a black dot at the specified coordinates
        for (int x = dotX - (dotRadius / 2); x <= dotX + dotRadius / 2; x++)
        {
            for (int y = dotY - dotRadius / 2; y <= dotY + dotRadius / 2; y++)
            {
                if (x >= 0 && x < imageWidth && y >= 0 && y < imageHeight)
                {
                    texture.SetPixel(x, y, Color.black);
                    Debug.Log("x: " + x + "y: " + y);
                }
            }
        }



        // Apply the changes to the texture
        texture.Apply();

        // Encode the texture as a PNG
        byte[] pngBytes = texture.EncodeToPNG();

        // Create the folder if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Save the PNG file to the specified folder
        string filePath = Path.Combine(folderPath, "generated_image.png");
        File.WriteAllBytes(filePath, pngBytes);

        // Clean up the texture
        Destroy(texture);

        Debug.Log("Image generated and saved to: " + filePath);


    }
}
