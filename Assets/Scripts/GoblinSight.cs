using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSight : MonoBehaviour
{
    public GoblinRefrences R;
    public void Start()
    {
        StartCoroutine(FOVRoutine());
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FeildOfViewCheck();
        }
    }
    private void FeildOfViewCheck()
    {
        Collider[] FoodCheck = Physics.OverlapSphere(transform.position, R.radius, R.targetMask);

        if(FoodCheck.Length != 0)
        {
            Transform food = FoodCheck[0].transform;
            Vector3 directionToFood = (food.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToFood) < R.angle / 2)
            {
                float distanceToFood = Vector3.Distance(transform.position, food.position);

                if(!Physics.Raycast(transform.position, directionToFood, distanceToFood, R.obstructionMask))
                    R.canSeeFood = true;
                else
                    R.canSeeFood = false;
            }
            else   
                R.canSeeFood = false;
        }

        Collider[] DangerCheck = Physics.OverlapSphere(transform.position, R.radius, R.DangerMask);

        if(DangerCheck.Length != 0)
        {
            Transform enemy = DangerCheck[0].transform;
            Vector3 directionToEnemy = (enemy.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToEnemy) < R.angle / 2)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

                if(!Physics.Raycast(transform.position, directionToEnemy, distanceToEnemy, R.obstructionMask))
                    R.canSeeEnemy = true;
                else
                    R.canSeeEnemy = false; 
            }
            else   
                R.canSeeEnemy = false;
        }
    }
}
