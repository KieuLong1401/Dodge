using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth;
    public TextMeshProUGUI healthText;
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
            healthText.text = $"{health} / {maxHealth}";
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if(playerController)
            {
                playerController.Die();
            }
        }
    }

    public void Recover(float HP)
    {
        health += HP;
        
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
