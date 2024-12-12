using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    public float speed;
    public float damage;
    private Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            healthBar.TakeDamage(damage);

        }

        // if(other.CompareTag("Wall"))
        // {
        //     Destroy(gameObject);
        // }
    }

    public void ChangeMoveDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);

        rigidBody.AddForce(direction * speed, ForceMode.Impulse);
    }
}
