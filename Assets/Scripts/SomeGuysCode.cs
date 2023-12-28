using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeGuysCode : MonoBehaviour
{
    /*
    public float lookRadius = 10f;  

    Transform target; 
    //UnityEngine.AI.NavMeshAgent agent; 
    //CharacterCombat combat;

    //private BaseEnemyAnimator baseEnemyAnimator;

    private float wanderSpeed = 2f;
    public float normalSpeed;
    public int roamRadius=70;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        normalSpeed = agent.speed;
        baseEnemyAnimator = GetComponent<BaseEnemyAnimator>();
    }

    //Vector3(transform.position.x + Random.Range(-x, x), 0, transform.position.y + Random.Range(-x, x)


    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);



        //if not inside the lookRadius
        if (distance >= lookRadius)
        {
            agent.speed = wanderSpeed;
            baseEnemyAnimator.IsWandering();
        }



        // If inside the lookRadius
        if (distance <= lookRadius)
        {
                // Move towards the target
                agent.SetDestination(target.position);
                agent.speed = normalSpeed;
                baseEnemyAnimator.IsNotWandering();

                // If within attacking distance
                if (distance <= agent.stoppingDistance)
                {
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                    }

                    FaceTarget();   // Make sure to face towards the target
                }
        }
    }

        // Rotate to face the target
        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }




        // Show the lookRadius in editor
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
    */
}