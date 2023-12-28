using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    void Start()
    {
        //currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
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
            if(maxHealth >= currentHealth)
            {
                currentHealth = maxHealth;
            }
        }
        else
        {
            currentHealth = maxHealth;
        }
    }
}
