using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotatoMine : MonoBehaviour
{
    public float activationTime = 5f;
    public float proximityTimeToEat = 1f;
    public float explosionDamage = 5f;
    public float inactiveY = -0.5f;
    public float activeY = 0f;

    private bool isActive = false;
    private bool isPlayerNearby = false;
    private float proximityTimer = 0f;

    private float remainingTime;

    public float eatingRadius = 1.5f;
    public GameObject eatingArea;
    public GameObject eatingAreaTimer;

    public LayerMask layerMask;

    void Start()
    {
        SetVerticalPosition(inactiveY);

        remainingTime = activationTime;

        StartCoroutine(ActivateAfterDelay());
    }

    void Update()
    {
        if (!isActive)
        {
            remainingTime -= Time.deltaTime;
            
            if (isPlayerNearby)
            {
                proximityTimer += Time.deltaTime;

                if (proximityTimer >= proximityTimeToEat)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                proximityTimer = 0f;
            }
        }


        Collider[] collider = Physics.OverlapSphere(transform.position, eatingRadius, layerMask);

        if(collider.Length > 0)
        {
            isPlayerNearby = true;
        }
        else
        {
            isPlayerNearby = false;
        }

        if(eatingArea != null) {
        }

        if(eatingAreaTimer != null)
        {
            float scale = Mathf.Clamp01(proximityTimer / proximityTimeToEat);
            eatingAreaTimer.transform.localScale = new Vector3(scale, 0.01f, scale);
        }

    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(activationTime);
        ActivateObject();
    }

    void ActivateObject()
    {
        isActive = true;
        SetVerticalPosition(activeY);
        Destroy(eatingArea);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActive)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (player != null)
        {
            healthBar.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }

    void SetVerticalPosition(float yPosition)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = yPosition;
        transform.position = currentPosition;
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(transform.position, eatingRadius);
    // }   
}
