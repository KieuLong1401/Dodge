using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth;
    private float health;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        if(health <= 0)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if(playerController)
            {
                playerController.Die();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
