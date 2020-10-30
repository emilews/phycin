using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Phycin : MonoBehaviour {

    private Quaternion newRot;
    private bool changed = false;

    private void Awake() {
    }
    private void FixedUpdate() {
        if (changed) {
            GetComponent<Transform>().rotation = newRot;
            changed = false;
        }
    }

    public void setNewRot(Quaternion nR) {
        newRot = nR;
        changed = true;
    }

}