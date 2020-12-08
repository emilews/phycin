using UnityEditor;
using UnityEngine;

public class PhycinEditorTool : EditorWindow {
    /**
     * 
     *
     */
    private bool useLandscape;
    private string localIP;
    private bool invertAccel;
    private bool invertGyro;

    [MenuItem("Phycin/Server")]
    public static void ShowWindow() {
        GetWindow(typeof(PhycinEditorTool));
    }

    private void OnGUI() {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        GUILayout.Label("Local IP", EditorStyles.label);
        useLandscape = EditorGUILayout.Toggle("Use Landscape", useLandscape);
        //camera = (GameObject)EditorGUILayout.ObjectField("Camera", camera, typeof(GameObject), true);
        if (GUILayout.Button("Start")) {
            StartServer();
        } 
        if (GUILayout.Button("Stop")) {
            StopServer();
        }

    }


    private void StartServer() {
        PhycinServer.PhycinServer.StartPhycinServer();
    }
    private void StopServer() {
        PhycinServer.PhycinServer.StopPhycinServer();
    }
  
}
