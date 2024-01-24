using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnareTrap : MonoBehaviour
{
    public GameObject TriggerdSnareTrap;
    public float maxDamage = 50f;
    public float minDamage = 10f;
    [HideInInspector] public float Damage;

    void Start()
    {
        Damage = Random.Range(maxDamage, minDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Damage);
                Instantiate(TriggerdSnareTrap, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
                Instantiate(TriggerdSnareTrap, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
