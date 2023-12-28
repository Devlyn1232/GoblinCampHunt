using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public GoblinRefrences R;
    public GoblinMovement M;
    private bool Search = true;
    // Start is called before the first frame update
    public void JumpAttack()
    {
        R.CloseCanAttack = false;
        //print("Jumped");
        StartCoroutine(jumpAttack());
    }
    public IEnumerator jumpAttack()
    {
        Vector3 targetDirection = R.FindClosestEnemy().transform.position - transform.position;
        float distanceToTarget = targetDirection.magnitude;
        targetDirection.Normalize();
        float jumpForce = 15.0f;
            
        Vector3 jumpVelocity = targetDirection *jumpForce;

        R.rigidbod.AddForce(jumpVelocity, ForceMode.VelocityChange);
        R.rigidbod.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode.Acceleration);
        new WaitForSeconds(.5f);

        PlayerCombat playertCombat = R.FindClosestEnemy().GetComponent<PlayerCombat>();
        if (playertCombat != null)
        {
            if (playertCombat.BlockIntegredy <= R.JumpAttackBlockDamage)
            {
                playertCombat.BlockIntegredy -= R.JumpAttackBlockDamage;
            }
            else
            {
                PlayerHealth playerHealth = R.FindClosestEnemy().GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(R.JumpAttackDamage);
            }
            R.rigidbod.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }
        yield return new WaitForSeconds(R.JumpAttackCooldown);
        
        R.CloseCanAttack = true;
    }

    public void RockThrow()
    {
        R.RangeCanAttack = false;
        //print("ThrewRpck");
        StartCoroutine(rockThrow());
    }
    public IEnumerator rockThrow()
    {
        GameObject newRock = Instantiate(R.RockThrow, R.RockThrowPoint.transform.position, transform.rotation);
        Rigidbody rockRigidbody = newRock.GetComponent<Rigidbody>();

        if (rockRigidbody != null)
        {
            // Apply force in a specific direction to the instantiated rock
            Vector3 targetDirection = R.FindClosestEnemy().transform.position - transform.position;
            float distanceToTarget = targetDirection.magnitude;
            targetDirection.Normalize();
            targetDirection.y = 0.2f;
            targetDirection.Normalize();
            float throwForce = 20.0f;
            
            Vector3 throwVelocity = targetDirection *throwForce;

            rockRigidbody.AddForce(throwVelocity, ForceMode.VelocityChange);
            rockRigidbody.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode.Acceleration);
            PlayerCombat playertCombat = R.FindClosestEnemy().GetComponent<PlayerCombat>();
            if (playertCombat.Blocking && playertCombat.BlockIntegredy <= R.RockThrowBlockDamage)
            {
                playertCombat.BlockIntegredy -= R.RockThrowBlockDamage;
            }
            else
            {
                if (newRock.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    PlayerHealth playerHealth = R.FindClosestEnemy().GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(R.RockThrowDamage);
                }
            }
        }
        yield return new WaitForSeconds(R.RockThrowCooldown);
        R.RangeCanAttack = true;
        
    }

    public void CantFindEnemy()
    {
        if (Search)
            StartCoroutine(cantFindEnemy());
    }
    public IEnumerator cantFindEnemy()
    {
        Search = false;
        float timer = 0f;
        float maxWaitTime = 5f; // Maximum time to wait for seeing an enemy
        //R.navMeshagent.stoppingDistance = 0;

        while (!R.canSeeEnemy && timer < maxWaitTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (R.canSeeEnemy)
        {
            // Stop spinning or any other action you want to take
            //R.spin = false;
            R.AttackMode = true; // Set AttackMode to true or false based on your logic
            R.navMeshagent.stoppingDistance = R.leapDistance;
            Search = true;
        }
        else
        {
            R.Encounters += 1;
            //R.spin = true; // Continue spinning or any other action if enemy is not found
            R.AttackMode = false; // Set AttackMode to false if enemy is not found
            R.navMeshagent.stoppingDistance = R.leapDistance;
        }
    }

    
}
