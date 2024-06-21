using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class VRDataSender : MonoBehaviour
{
    public Transform headsetTransform; // Drag the OVRCameraRig or CenterEyeAnchor here

    private UdpClient udpClient;
    private string serverIp = "192.168.137.204"; // Change this to your computer's IP
    private int port = 8888; // Choose any port number

    void Start()
    {
        udpClient = new UdpClient();
    }

    void Update()
    {
        Vector3 headsetPosition = headsetTransform.position;
        Quaternion headsetRotation = headsetTransform.rotation;
        string data = headsetPosition.x + "," + headsetPosition.y + "," + headsetPosition.z + ","
                      + headsetRotation.x + "," + headsetRotation.y + "," + headsetRotation.z + "," + headsetRotation.w;
        SendData(data);
        Debug.Log("will this print?");
    }

    void SendData(string data)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        udpClient.Send(bytes, bytes.Length, serverIp, port);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}
