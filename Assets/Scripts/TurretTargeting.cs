using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    RaycastHit hit;
    public float degreesPerSecond;
    public GameObject ShootParticle;
    public ParticleSystem partSystem;
    public bool CanAttack = true;
    public float Damage;
    public float shootCoolDown;
    public LayerMask PlayerLayerMask;
    public float range = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        bool AttackPlayer = Physics.CheckSphere(transform.position, range, PlayerLayerMask);
        if (AttackPlayer)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, PlayerLayerMask) && hit.collider.CompareTag("Player"))
            {
                if (CanAttack)
                {
                    Debug.DrawRay(transform.position, transform.forward * Mathf.Infinity, Color.green);
                    StartCoroutine(DamageRaycast());
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.forward * range, Color.red);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * range, Color.white);
            }

            Vector3 targetsDirection = GetTargetPosition() - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(targetsDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime * degreesPerSecond);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawWireSphere(transform.position, range);
    }

    Vector3 GetTargetPosition()
    {
        return FindClosestPlayer().transform.position;
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = player;
                distance = curDistance;
            }
        }

        return closest;
    }

    IEnumerator DamageRaycast()
    {
        CanAttack = false;

        PlayerHealth enemyHealth = FindClosestPlayer()?.GetComponent<PlayerHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(Damage);
        }

        ParticleSystem.ShapeModule psShape = partSystem.shape;
        ParticleSystem psMain = partSystem;

        psShape.position = new Vector3(0, 0, range / 2);
        psShape.scale = new Vector3(0, 0, range);
        
        ParticleSystem.MainModule psMainModule = partSystem.main;
        psMainModule.maxParticles = Mathf.RoundToInt(range * 10);

        yield return new WaitForSeconds(shootCoolDown);
        CanAttack = true;
    }
}