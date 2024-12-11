using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hipnoshroom : MonoBehaviour
{
    public float reverseTime;    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.ReverseMovement(reverseTime);

            Destroy(gameObject);
        }
    }
}
