using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireHeal : MonoBehaviour
{
    public float healthAmount = 5f;
    public float healInterval = 1f;
    public bool canHeal = true;
    [SerializeField] public GameStates GS;
    void Awake()
    {
        GS = FindObjectOfType<GameStates>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (canHeal && !GS.inCombat)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    canHeal = false;
                    StartCoroutine(HealPlayerOverTime(playerHealth));
                }
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    canHeal = false;
                    StartCoroutine(HealEnemyOverTime(enemyHealth));
                }
            }
        }
    }

    private IEnumerator HealPlayerOverTime(PlayerHealth playerHealth)
    {
        playerHealth.Heal(healthAmount);
        yield return new WaitForSeconds(healInterval);
        canHeal = true;
    }

    private IEnumerator HealEnemyOverTime(EnemyHealth enemyHealth)
    {
        enemyHealth.Heal(healthAmount);
        yield return new WaitForSeconds(healInterval);
        canHeal = true;
    }
}
