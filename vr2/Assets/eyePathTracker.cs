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

    private string folderPath = "Assets/GeneratedImages/";
    private string filePath = "Assets/GeneratedPath/path.txt";

    // Size of the dot (radius)
    public int dotSize = 4;

    private int arraySize = 300;
    private Vector2[] vectorArray;
    private int loopCount = 0;



    // Start is called before the first frame update
    void Start()
    {

        vectorArray = new Vector2[arraySize];

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
        if (loopCount > arraySize - 1)
        {
            loopCount = 0;
            //drawPath(); too laggy
            exportPath();
        }

    }

    private void drawPath()
    {
        for (int i = 0; i < vectorArray.Length; i++)
        {
            Vector2 currentVector = vectorArray[i];
            GenerateAndSaveImage(imageWidth, imageHeight, (int)vectorArray[i].x, (int)vectorArray[i].y, dotSize, folderPath);
            //Debug.Log("Element " + i + ": (" + currentVector.x + ", " + currentVector.y + ")");
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

        Debug.Log("Vector2 array exported to: " + filePath);
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

        // Create or load the existing texture
        Texture2D texture;
        string filePath = Path.Combine(folderPath, "generated_image.png");
        if (File.Exists(filePath))
        {
            byte[] existingBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(width, height);
            texture.LoadImage(existingBytes); // Load existing image
        }
        else
        {
            // Create a new texture if no existing image found
            texture = new Texture2D(width, height);
        }

        // Draw a black dot at the specified coordinates
        Color32[] pixels = texture.GetPixels32();
        for (int x = dotX - (dotRadius / 2); x <= dotX + dotRadius / 2; x++)
        {
            for (int y = dotY - dotRadius / 2; y <= dotY + dotRadius / 2; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    // Only draw if the pixel is within the bounds of the texture
                    int index = y * width + x;
                    pixels[index] = Color.black; // Draw black dot
                }
            }
        }

        // Apply the changes to the texture
        texture.SetPixels32(pixels);
        texture.Apply();

        // Encode the texture as a PNG
        byte[] pngBytes = texture.EncodeToPNG();

        // Save the PNG file to the specified folder
        File.WriteAllBytes(filePath, pngBytes);

        Debug.Log("Image generated and saved to: " + filePath);
    }

}
