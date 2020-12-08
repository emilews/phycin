using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace PhycinServer {

    public class Phycins : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e) {
            string msg = e.Data;
            string[] msgs = msg.Split('>');
            float[] values = new float[4];
            for(int i = 0; i <= 3; i++) {
                values[i] = float.Parse(msgs[1].Split(';')[0].Split(':')[i]);
            }
            PhycinServer.UpdateCameraRotation(values);
        }
        protected override void OnOpen() {
            UnityEngine.Debug.Log("Conexión nueva.");
        }

    }

    public class PhycinServer {

        private static WebSocketServer wss = null;
        private static bool isRunning = false;
        private static GameObject cameraFromEditor;
        public static void StartPhycinServer(){
            if(wss == null) {
                wss = new WebSocketServer("ws://"+GetLocalIPAddress());
                wss.AddWebSocketService<Phycins>("/");
                wss.Start();
                isRunning = true;
                UnityEngine.Debug.Log("Server started.");
            } else if(!isRunning){
                wss.Start();
                isRunning = true;
                UnityEngine.Debug.Log("Server started.");
            }
        }
        public static void StopPhycinServer() {
            if(wss == null) {
                //Nothing
                UnityEngine.Debug.Log("Server not stopped, was not initialised.");
            } else if(isRunning){
                wss.Stop();
                isRunning = false;
                UnityEngine.Debug.Log("Server stopped.");
            }
        }

        public static void UpdateCameraRotation(float[] values) {
            Debug.Log("X:" + values[0] + " Y:" + values[1] + " Z:" + values[2] + " W:" + values[3]);
            Quaternion quat = new Quaternion(values[0], values[1], values[2], values[3]);
            Camera.main.enabled = true;
            Debug.Log(Camera.main.GetComponent<PhycinComponent>().getSome());
        }

        // Won't work if there's a virtual adapter installed (e.g. 'VirtualBox Host-Only Network').
        public static string GetLocalIPAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}