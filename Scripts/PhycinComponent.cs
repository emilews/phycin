using UnityEngine;

public class PhycinComponent : MonoBehaviour {

    public Quaternion newRot = new Quaternion(0, 0, 0, 0);


    private void Awake() {
        Debug.Log(GetInstanceID() + "From object");
    }

    private void FixedUpdate() {
        GetComponent<Transform>().rotation = Quaternion.Slerp(GetComponent<Transform>().rotation, newRot, Time.deltaTime * 0.5f);
    }

    public void setNewRot(Quaternion nR) {
        Debug.Log("Entered");
        newRot = nR;
    }

    public string getSome() {
        return "ReturnedSomething";
    }

}