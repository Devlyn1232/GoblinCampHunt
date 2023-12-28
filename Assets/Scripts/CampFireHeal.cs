using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireHeal : MonoBehaviour
{
    public float healthAmount = 5f;
    public float healInterval = 1f;
    public bool canHeal = true;

    private void OnTriggerStay(Collider other)
    {
        if (canHeal)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    StartCoroutine(HealPlayerOverTime(playerHealth));
                }
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    StartCoroutine(HealEnemyOverTime(enemyHealth));
                }
            }
        }
    }

    private IEnumerator HealPlayerOverTime(PlayerHealth playerHealth)
    {
        canHeal = false;
        while (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.Heal(healthAmount);
            yield return new WaitForSeconds(healInterval);
        }
        canHeal = true;
    }

    private IEnumerator HealEnemyOverTime(EnemyHealth enemyHealth)
    {
        canHeal = false;
        while (enemyHealth.currentHealth < enemyHealth.maxHealth)
        {
            enemyHealth.Heal(healthAmount);
            yield return new WaitForSeconds(healInterval);
        }
        canHeal = true;
    }
}
