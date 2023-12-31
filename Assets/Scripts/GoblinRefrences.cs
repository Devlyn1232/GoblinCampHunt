using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class GoblinRefrences : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidbod;
    [HideInInspector] public Animator animator;
    public EnemyHealth enemyHealth;
    public float currentHealth;

    [Header("Stats")]
    public int Encounters = 0;
    public int MaxEncounters = 3;
    public GameObject deathParticle;
    
    
    [HideInInspector] public float leapDistance;
    public GameObject GoblinCamp;
    public GoblinCamp GoblicCampScript;
    [HideInInspector] public bool spin;
    public GameObject RockThrow;
    public GameObject RockThrowPoint;
    public float minJumpAttackCooldown = 0.5f;
    public float maxJumpAttackCooldown = 2f;

    public float minRockThrowCooldown = 3f;
    public float maxRockThrowCooldown = 4f;

    public float JumpAttackCooldown { get; private set; }
    public float JumpAttackDamage = 5f;
    public float JumpAttackBlockDamage = 60f;
    public float RockThrowCooldown { get; private set; }
    public float RockThrowDamage = 4f;
    public float RockThrowBlockDamage = 40;

    [HideInInspector] public float directionChangeTimer = 0.0f;
    public float directionChangeInterval = 0.2f;
    //private float rotationSpeed = 60f;

    [HideInInspector] public NavMeshAgent navMeshagent;
    public bool CloseCanAttack;
    public bool RangeCanAttack;

    [Header("sight & pathing calculations")]
    public float radius;
    [Range(0,360)]
    public float angle;
    private float pathUpdateDeadLine;
    [HideInInspector] public float WanderRange;

    [Header("LayerMasks")]
    public LayerMask DangerMask; //player and player allies
    public LayerMask targetMask; // wildlife
    public LayerMask obstructionMask; //trees and rocks and stuff

    [Header("Sight")]
    public bool canSeeEnemy; //can see player
    public bool canSeeFood; //when see whild life
    public bool canSeeCamp; //when neer base

    [Header("Modes")]
    public bool AttackMode; //when see player
    public bool sneeking; //when is unseen and wants to kill
    public bool HuntMode; //when see wildlife
    public bool wandering; //when looking for food
    public bool returnToBase; //when done hunting or injured from fight
    //public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
        rigidbod = GetComponent<Rigidbody>();
        navMeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GoblinCamp = GameObject.FindGameObjectWithTag("GoblinCamp");
        GoblicCampScript = GoblinCamp.GetComponent<GoblinCamp>();
        if (GoblicCampScript != null)
        {
            WanderRange = GoblicCampScript.WanderRange;
        }
        JumpAttackCooldown = Random.Range(minJumpAttackCooldown, maxJumpAttackCooldown);
        RockThrowCooldown = Random.Range(minRockThrowCooldown, maxRockThrowCooldown);
        if (navMeshagent != null)
        {
            leapDistance = navMeshagent.stoppingDistance;
        }
        else
        {
            Debug.LogError("NavMeshAgent not found!");
        }
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }

        return closest;
    }
    public GameObject FindClosestFood()
    {
        GameObject[] animals = GameObject.FindGameObjectsWithTag("WeakPrey");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject animal in animals)
        {
            Vector3 diff = animal.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = animal;
                distance = curDistance;
            }
        }

        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawWireSphere(transform.position, leapDistance);
    }
}
