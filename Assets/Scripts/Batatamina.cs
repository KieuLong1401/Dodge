using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Batatamina : MonoBehaviour
{
    public float activationTime = 5f;
    public float proximityTimeToEat = 5f;
    public float explosionDamage = 5f;
    public float explosionRadius = 5f;
    public float inactiveY = 0.5f;
    public float activeY = 1.5f;

    private bool isActive = false;
    private bool isPlayerNearby = false;
    private float proximityTimer = 0f;

    public GameObject timerCanvasPrefab;
    private GameObject timerCanvas;
    private TextMeshProUGUI timerText;
    private float remainingTime;

    void Start()
    {
        SetVerticalPosition(inactiveY);

        remainingTime = activationTime;
        timerCanvas = Instantiate(timerCanvasPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        timerCanvas.transform.SetParent(transform);
        timerText = timerCanvas.GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(ActivateAfterDelay());
    }

    void Update()
    {
        if (!isActive)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(remainingTime).ToString();
        }

        if (isPlayerNearby && !isActive)
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

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(activationTime);
        ActivateObject();
    }

    void ActivateObject()
    {
        isActive = true;
        SetVerticalPosition(activeY);

        Destroy(timerCanvas);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActive)
            {
                Explode();
            }
            else
            {
                isPlayerNearby = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
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
}
