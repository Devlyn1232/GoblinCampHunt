using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public GoblinRefrences R;
    public UnityEvent Death;

    public float currentHealth;
    public float maxHealth;

    [SerializeField] EnemyHealthBar healthBar;

    void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        R.currentHealth = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        R.currentHealth = currentHealth;
        if (currentHealth <= 0)
        {
            Instantiate(R.deathParticle, transform.position, transform.rotation);
            //Death.Invoke();
            Destroy(gameObject);
        }
    }
    public void Heal(float Health)
    {
        if (currentHealth !>= maxHealth)
        {
            currentHealth += Health;
            R.currentHealth = currentHealth;
        }
        else
        {
            currentHealth = maxHealth;
            R.currentHealth = currentHealth;
        }
    }
}
