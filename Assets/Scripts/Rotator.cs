using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float reverseRateMin;
    public float reverseRateMax;
    public float speedMin;
    public float speedMax;
    private float rotationSpeed;

    void Start() {
        StartCoroutine(ReverseRotateDirection());
    }
    void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    IEnumerator ReverseRotateDirection() {
        while(true)
        {
            float randomReverseRate = Random.Range(reverseRateMin, reverseRateMax);
            float randomSpeed = Random.Range(speedMin, speedMax);

            if(rotationSpeed > 0)
            {
                rotationSpeed = -randomSpeed;
            }
            else
            {
                rotationSpeed = randomSpeed;
            }

            yield return new WaitForSeconds(randomReverseRate);
        }
    }
}