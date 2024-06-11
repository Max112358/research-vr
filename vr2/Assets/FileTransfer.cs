using System.IO;

public class FileTransfer : MonoBehaviour
{
    private void SaveFileOnHeadset()
    {
        string fileName = "example.txt";
        string content = "This is an example file.";

        // Specify the directory on the VR headset where you want to save the file
        string directoryPath = "/storage/emulated/0/Download/";

        // Combine the directory path and file name
        string filePath = Path.Combine(directoryPath, fileName);

        // Write the file to the specified path on the VR headset
        File.WriteAllText(filePath, content);

        Debug.Log("File saved on the VR headset: " + filePath);
    }
}