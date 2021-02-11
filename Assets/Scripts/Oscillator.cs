﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [SerializeField] 
    Vector3 movementVector;
    [Range(0,1)] [SerializeField]
    float movementFactor;
    Vector3 startingPosition;

    void Start() {
        startingPosition = transform.position;
    }
    void Update() {
        Vector3 offset = movementFactor * movementVector;

        transform.position = startingPosition + offset;
    }
}
