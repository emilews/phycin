using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace PhycinServer {

    public class Phycin : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e) {
            string msg = e.Data;
            string[] msgs = msg.Split('>');
            float[] values = new float[4];
            for(int i = 0; i < 3; i++) {
                values[i] = float.Parse(msgs[1].Split(';')[0].Split(':')[i]);
            }
            UnityEngine.Debug.Log(values[0]);
        }
        protected override void OnOpen() {
            UnityEngine.Debug.Log("Conexión nueva.");
        }

    }

    public class PhycinServer {

        private static WebSocketServer wss = null;
        private static bool isRunning = false;
        public static void StartPhycinServer(){
            if(wss == null) {
                wss = new WebSocketServer("ws://"+GetLocalIPAddress());
                wss.AddWebSocketService<Phycin>("/");
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