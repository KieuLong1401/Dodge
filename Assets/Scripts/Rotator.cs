using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float rotationSpeed = 10f;

    void Update() {
        transform.Rotate(0f, rotationSpeed * (Time.deltaTime / 10) * Time.deltaTime, 0f);
    }
}