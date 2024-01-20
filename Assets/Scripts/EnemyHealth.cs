using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public GoblinRefrences R;
    public GoblinMovement M;
    public GameStates GS;
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
        // Assign the GameStates script reference using FindObjectOfType during runtime
        GS = FindObjectOfType<GameStates>();
        
        if (GS == null)
        {
            Debug.LogError("GameStates script not found in the scene!");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        R.currentHealth = currentHealth;
        if (!R.canSeeEnemy)
        {
            M.dashAndLookAtPlayer();
        }
        if (currentHealth <= 0)
        {
            GS.combatIminant = false;
            GS.inCombat = false;
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
