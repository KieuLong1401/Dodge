using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public float recoverHP;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            healthBar.Recover(recoverHP);

            Destroy(gameObject);
        }
    }
}
