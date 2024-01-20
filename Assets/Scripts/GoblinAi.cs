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
        // Assign the GameStates script reference using FindObjectOfType during runtime
        GS = FindObjectOfType<GameStates>();
        
        if (GS == null)
        {
            Debug.LogError("GameStates script not found in the scene!");
        }
    }

    public void FixedUpdate()
    {
        if (!R.AttackMode && !R.HuntMode &&!R.returnToBase)
        {
            randomMovement.enabled = true;
            R.wandering = true;
            bool InCampRange = Vector3.Distance(transform.position, R.GoblinCamp.transform.position) <= R.WanderRange;

            if (!InCampRange)
            {
                R.returnToBase = true;
            }
        }
        else
        {
            randomMovement.enabled = false;
            R.wandering = false;
        }


        if (R.canSeeFood && !R.AttackMode)
        {
            GameObject food = R.FindClosestFood();
            if (food != null && R.navMeshagent != null && !R.AttackMode)
            {
                R.navMeshagent.SetDestination(food.transform.position);
            }
        }

        if (R.canSeeEnemy)
        {
            GS.combatIminant = true;
            if(R.returnToBase == true)
            {
                R.returnToBase = false;
            }
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

        if (R.canSeeFood && !R.AttackMode)
        {
            R.HuntMode = true;
        }
        else
        {
            R.HuntMode = false;
        }
    }
}