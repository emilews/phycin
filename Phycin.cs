using UnityEngine;
/// <summary>
/// Forefront class for the server communication.
/// </summary>
public class Phycin : MonoBehaviour {
    // Server IP address
    [SerializeField]
    private string hostIP;
    // Server port
    [SerializeField]
    private int port = 8000;
    // Flag to use localhost
    [SerializeField]
    private bool useLocalhost = false;
    // Address used in code
    private string host => useLocalhost ? "localhost" : hostIP;
    // Final server address
    private string server;
    // WebSocket Client
    private WSClient client;
    /// <summary>
    /// Unity method called on initialization
    /// </summary>
    private void Awake() {
        server = "ws://" + host + ":" + port;
        client = new WSClient(server);
        ConnectToServer();
    }
    /// <summary>
    /// Unity method called every frame
    /// </summary>
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
    /// <summary>
    /// Method responsible for handling server messages
    /// </summary>
    /// <param name="msg">Message.</param>
    private void HandleMessage(string msg) {
        Debug.Log("Server: " + msg);
    }
    /// <summary>
    /// Call this method to connect to the server
    /// </summary>
    public async void ConnectToServer() {
        await client.Connect();
    }
    /// <summary>
    /// Method which sends data through websocket
    /// </summary>
    /// <param name="message">Message.</param>
    public void SendRequest(string message) {
        client.Send(message);
    }
}