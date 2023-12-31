using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public GoblinRefrences R;
    public GoblinAi I;
    private bool firstTimeInAttackRange = true;
     // Change direction every 2 seconds
    private Vector3 currentDirection;

    void Update()
    {
        if (R.spin)
        {
            float rotationSpeed = 60f; // Adjust the rotation speed as needed
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (R.canSeeEnemy)
        {
            R.navMeshagent.SetDestination(R.FindClosestEnemy().transform.position);

            bool InAttackRange = Physics.CheckSphere(transform.position, R.leapDistance +2, R.DangerMask);
            if (InAttackRange)
            {
                if(firstTimeInAttackRange)
                {
                    ChangeDirection();
                    firstTimeInAttackRange = false;
                }
                // Example code to move when in attack range (adjust to your logic)
                R.directionChangeTimer += Time.deltaTime;

                if (R.directionChangeTimer >= R.directionChangeInterval)
                {
                    R.directionChangeTimer = 0.0f; // Reset timer
                    ChangeDirection();
                }

                float moveSpeed = 6.0f; // Adjust the speed as needed

                // Move the Goblin along the chosen direction
                transform.Translate(currentDirection * moveSpeed * Time.deltaTime);
            }
            bool InRunRange = Physics.CheckSphere(transform.position, R.leapDistance -1, R.DangerMask);
            if (InRunRange)
            {
                float moveSpeed = 8.0f;
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                ChangeDirection();
            }
        }
        if (R.returnToBase)
        {
            R.navMeshagent.SetDestination(R.GoblinCamp.transform.position);
        }
    }

    // Function to change the direction
    public void ChangeDirection()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue >= 0.5f)
        {
            currentDirection = Vector3.left; // Move left if random value is >= 0.5
            
        }
        else
        {
            currentDirection = Vector3.right; // Move right if random value is < 0.5
            
        }
    }

    public void dashAndLookAtPlayer()
    {
        StartCoroutine(DLAP());
    }
    public IEnumerator DLAP()
    {
        R.AttackMode = true;
        Vector3 targetDirection = R.FindClosestEnemy().transform.position - transform.position;
        float distanceToTarget = targetDirection.magnitude;
        //targetDirection.y = 0f;
        targetDirection.Normalize();
        float jumpForce = 15.0f;
            
        Vector3 jumpVelocity = -targetDirection *jumpForce;

        R.rigidbod.AddForce(jumpVelocity, ForceMode.VelocityChange);
        R.rigidbod.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode.Acceleration);
        new WaitForSeconds(.5f);
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, R.navMeshagent.angularSpeed);
        yield return new WaitForSeconds(2f);
    }
}
