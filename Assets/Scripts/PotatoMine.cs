using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotatoMine : MonoBehaviour
{
    public float activationTime;
    public float proximityTimeToEat;
    public float explosionDamage;
    public float inactiveY;
    public float activeY;

    private bool isActive = false;
    private bool isPlayerNearby = false;
    private float proximityTimer = 0f;

    private float remainingTime;

    public float eatingRadius;
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
                remainingTime -= Time.deltaTime;
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
        Debug.Log("asdf");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, eatingRadius);
    }   
}
