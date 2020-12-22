using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    void Update() {
        ProcessInput();
    }

    void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            print("Thrust");
        }
        
        if (Input.GetKey(KeyCode.A)) {
            print("Rotating Left");
        } else if (Input.GetKey(KeyCode.D)) {
            print("Rotating Right");
        }
    }
}
