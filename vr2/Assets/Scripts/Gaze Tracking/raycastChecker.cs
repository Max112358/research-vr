using System.Xml;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class RaycastChecker : MonoBehaviour
{
    // The tag of the quad to check against
    public string targetTag = "visualTarget";
    private Dictionary<string, int> objectScores = new Dictionary<string, int>();
    private GameObject displayGO;
    private TextMeshPro displayText;
    public string filePathStart = "Assets/View_Ratios/";
    private string filePath;
    // Define the file path
    //private string filePath;



    void Start()
    {
        // Find the TextMeshPro component in the scene
        displayGO = GameObject.FindGameObjectWithTag("displayText");
        displayText = displayGO.GetComponent<TextMeshPro>();


        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = "view_ratio_" + dateTime + ".txt";
        filePath = (filePathStart + fileName);
        Debug.Log("filePath: " + filePath);
        //filePath = Path.Combine(filePath, fileName);

    }


    void Update()
    {
        // Define the origin of the ray and its direction
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            // Check if the hit object has the specific tag
            if (hit.collider.CompareTag(targetTag))
            {
                // Debug.Log("Raycast hit the specific quad!");
                GameObject hitObject = hit.collider.gameObject;

                // Get the name of the GameObject
                string objectName = hitObject.name;
                //Debug.Log("objectName: " + objectName);


                AddOrAccumulate(objectScores, objectName, 1);
                displayText.text = objectName;


                //attentionCount += 1;

            }
            else
            {
                AddOrAccumulate(objectScores, "Nothing", 1);
                displayText.text = "Nothing";
            }
        }
        else
        {
            // Increment inAttentionCount if the raycast does not hit anything
            AddOrAccumulate(objectScores, "Nothing", 1);
            displayText.text = "Nothing";
        }

        // Ensure that both attentionCount and inAttentionCount are floats for the division
        //float finalRatio = (float)attentionCount / (attentionCount + inAttentionCount);
        //string ratioString = finalRatio.ToString("F2"); // Format the ratio to 2 decimal places
        //displayText.text = ratioString;




        /*
        int totalScore = 1;
        // Loop through the dictionary and sum the values
        foreach (KeyValuePair<string, int> kvp in objectScores)
        {
            totalScore += kvp.Value;
        }

        string key = "instruction surface";
        if (objectScores.ContainsKey(key))
        {
            int value = objectScores[key];
            //Debug.Log("Value at key '" + key + "': " + value);  // Output: Value at key 'Bob': 200
                                                                // Ensure that both attentionCount and inAttentionCount are floats for the division
            float finalRatio = (float)value / totalScore;
            string ratioString = finalRatio.ToString("F2"); // Format the ratio to 2 decimal places
            displayText.text = ratioString;
        }
        else
        {
            //Debug.Log("Key '" + key + "' not found in the dictionary.");
            displayText.text = "0";
        }
        */



    }

    void OnApplicationQuit()
    {
        // Perform your custom actions here
        //Debug.Log("Application is quitting!");
        // Example: Save game state
        //PrintDictionary(objectScores);


        // Print the dictionary to the file
        PrintDictionaryToFile(objectScores, filePath);
    }

    void AddOrUpdate(Dictionary<string, int> dictionary, string key, int value)
    {
        // Check if the key exists
        if (dictionary.ContainsKey(key))
        {
            // Update the existing value
            dictionary[key] = value;
        }
        else
        {
            // Add the new key-value pair
            dictionary.Add(key, value);
        }
    }

    void AddOrAccumulate(Dictionary<string, int> dictionary, string key, int value)
    {
        // Check if the key exists
        if (dictionary.ContainsKey(key))
        {
            // Add to the existing value
            dictionary[key] += value;
        }
        else
        {
            // Add the new key-value pair
            dictionary.Add(key, value);
        }
    }

    void PrintDictionary(Dictionary<string, int> dictionary)
    {
        foreach (KeyValuePair<string, int> kvp in dictionary)
        {
            Debug.Log(kvp.Key + ": " + kvp.Value);
        }
    }

    void PrintDictionaryToFile(Dictionary<string, int> dictionary, string path)
    {
        int totalScore = 1;
        // Loop through the dictionary and sum the values
        foreach (KeyValuePair<string, int> kvp in objectScores)
        {
            totalScore += kvp.Value;
        }

        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (KeyValuePair<string, int> kvp in dictionary)
            {
                // Calculate the fraction as a percentage
                double fraction = (double)kvp.Value / totalScore * 100;

                writer.WriteLine($"{kvp.Key}: {kvp.Value} ({fraction:F2}%)");
            }
        }

        Debug.Log("Dictionary written to " + path);
    }

}
