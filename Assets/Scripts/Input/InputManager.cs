using UnityEngine;
using System.Collections;

public abstract class InputManager : MonoBehaviour {
    public float Direction { get; protected set; }
    public bool StartPressed { get; protected set; }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CheckInputs();
    }

    protected abstract void CheckInputs();
}
