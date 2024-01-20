using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    [SerializeField] PlayerHealthBar healthBar;
    void Awake()
    {
        healthBar = GetComponentInChildren<PlayerHealthBar>();
    }
    void Start()
    {
        //currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        /*
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        */
    }
    public void Heal(float Health)
    {
        if (maxHealth >= currentHealth)
        {
            currentHealth += Health;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            if(maxHealth <= currentHealth)
            {
                currentHealth = maxHealth;
                healthBar.UpdateHealthBar(currentHealth, maxHealth);
            }
        }
    }
}
