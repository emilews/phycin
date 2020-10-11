using UnityEngine;

public class Phycin : MonoBehaviour {
    [SerializeField]
    private string hostIP;
    [SerializeField]
    private int port = 8000;
    [SerializeField]
    private bool useLocalhost = false;
    [SerializeField]
    private Camera camera;
    private string host => useLocalhost ? "localhost" : hostIP;
    private string server;
    private WSClient client;
   
    private void Awake() {
        server = "ws://" + host + ":" + port;
        client = new WSClient(server);
        ConnectToServer();
    }
    private void Update() {
        // Check if server send new messages
        var cqueue = client.receiveQueue;
        string msg;
        while (cqueue.TryPeek(out msg)) {
            // Parse newly received messages
            cqueue.TryDequeue(out msg);
            HandleMessage(msg);
        }
    }
    private void HandleMessage(string msg) {
        Debug.Log("Server: " + msg);
        /*
        string[] accel_data = msg.Split(';')[0].Split('>');
        float xMov = float.Parse(accel_data[1]);
        float yMov = float.Parse(accel_data[2]);
        float zMov = float.Parse(accel_data[3]);
        Vector3 newPos = new Vector3(xMov, yMov, zMov);
        camera.GetComponent<Transform>().position = newPos;
        */
        string[] gyro_data = msg.Split(';')[0].Split('>');
        float xRot = float.Parse(gyro_data[1]);
        float yRot = float.Parse(gyro_data[2]);
        float zRot = float.Parse(gyro_data[3]);
        Quaternion newRot = new Quaternion(xRot, -yRot, zRot, 1);
        camera.GetComponent<Transform>().rotation = newRot;
    }
    public async void ConnectToServer() {
        await client.Connect();
    }
    public void SendRequest(string message) {
        client.Send(message);
    }
}