using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAi : MonoBehaviour
{
    public GoblinRefrences R; //R.
    public GoblinAttack A;
    public GoblinMovement M;
    public RandomMovement randomMovement;
    public GameStates GS;

    void Start()
    {
        R.returnToBase = false;
        // Assign the GameStates script reference using FindObjectOfType during runtime
        GS = FindObjectOfType<GameStates>();
        
        if (GS == null)
        {
            Debug.LogError("GameStates script not found in the scene!");
        }
    }

    public void FixedUpdate()
    {
        if (!R.AttackMode && !R.HuntMode && !R.returnToBase)
        {
            randomMovement.enabled = true;
            R.wandering = true;
            bool InCampRange = Vector3.Distance(transform.position, R.GoblinCamp.transform.position) <= R.WanderRange;
            /*
            if (!InCampRange)
            {
                R.returnToBase = true;
            }
            */
        }
        else
        {
            randomMovement.enabled = false;
            R.wandering = false;
        }

        if(R.currentHealth <= R.runAwayHealth)
        {
            R.returnToBase = true;
            R.navMeshagent.SetDestination(R.GoblinCamp.transform.position);
        }

        /*
        if(R.returnToBase)
        {
            
        }
        */

        if (R.canSeeFood && !R.AttackMode)
        {
            R.wandering = false;
            GameObject food = R.FindClosestFood();
            if (food != null && R.navMeshagent != null && !R.AttackMode)
            {
                bool InAttackRange = Physics.CheckSphere(transform.position, R.leapDistance, R.targetMask);
                if(InAttackRange)
                {
                    if (R.CloseCanAttack)
                    {
                        A.JumpAttack();
                    }
                }
                R.navMeshagent.SetDestination(food.transform.position);
                
            }
        }

        if (R.canSeeEnemy)
        {
            GS.combatIminant = true;
            R.AttackMode = true;
            bool InAttackRange = Physics.CheckSphere(transform.position, R.leapDistance+1, R.DangerMask);
            if(InAttackRange)
            {
                if (R.CloseCanAttack)
                {
                    A.JumpAttack();
                }
            }
            bool InRangeAttack = Physics.CheckSphere(transform.position, R.leapDistance+10, R.DangerMask);
            if(InRangeAttack && !InAttackRange)
            {
                if(R.RangeCanAttack)
                {
                    A.RockThrow();
                }
            }
        }
        if (!R.canSeeEnemy)
        {
            GS.combatIminant = false;
        }
        
        if (R.AttackMode)
        {
            GS.inCombat = true;
            R.inCombat = true;
            A.CantFindEnemy();
        }
        if (!R.AttackMode)
        {
            R.inCombat = false;
            GS.inCombat = false;
        }
    }
}