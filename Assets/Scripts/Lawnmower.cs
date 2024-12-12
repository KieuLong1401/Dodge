using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    public float speed;
    public float damage;
    public float delayTime;

    private Vector3 moveDirection;
    private GameObject spawnArea;
    private bool canMove = false;
    void Start()
    {
        StartCoroutine(DelayBeforeMove());
    }

    void Update()
    {
        if(spawnArea != null && moveDirection != null)
        {
            Vector3 worldDirection = spawnArea.transform.TransformDirection(moveDirection);

            transform.rotation = Quaternion.LookRotation(-worldDirection, Vector3.up);

            if(canMove)
            {
                transform.position += worldDirection.normalized * speed * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            healthBar.TakeDamage(damage);

        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DelayBeforeMove()
    {
        yield return new WaitForSeconds(delayTime);

        canMove = true;
    }

    public void ChangeMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void ChangeSpawnArea(GameObject spawnArea)
    {
        this.spawnArea = spawnArea;
    }
}
