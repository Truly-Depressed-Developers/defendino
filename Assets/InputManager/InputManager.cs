using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    static public InputActions actions;

    private void Awake() {
        actions = new InputActions();
        actions.Enable();
    }
}
