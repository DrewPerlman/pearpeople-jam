using UnityEngine;
using System.Collections;

public class KeyboardInputManager : InputManager {
    [SerializeField] private KeyCode moveLeft = KeyCode.None;
    [SerializeField] private KeyCode moveRight = KeyCode.None;
    [SerializeField] private KeyCode startButton = KeyCode.None;

    // On start, check that the KeyCodes are set 
    private void Start() {
        if (moveLeft == KeyCode.None || moveRight == KeyCode.None
            || startButton == KeyCode.None) {
            Debug.Log("Controls not fully mapped!");
        }
    }

    override protected void CheckInputs() {
        if (Input.GetKey(moveLeft) && Input.GetKey(moveRight)) {
            Direction = 0;
        }
        else if (Input.GetKey(moveLeft)) {
            Direction = -1;
        }
        else if (Input.GetKey(moveRight)) {
            Direction = 1;
        }
        else {
            Direction = 0;
        }
        StartPressed = Input.GetKey(startButton);
    }
}