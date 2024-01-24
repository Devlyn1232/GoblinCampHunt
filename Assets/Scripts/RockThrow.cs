using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    private PlayerCombat P;
    public bool IsAttacking;
    public bool CanAttack = true;
    public float attackCooldown = .5f; 
    public GameObject Player;
    public PlayerResources PR;
    public GameObject Rock;

    void Start()
    {
        P = GetComponent<PlayerCombat>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PR = Player.GetComponent<PlayerResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if(CanAttack && PR.rock >= 1)
            {
                Attack();
                PR.rock --;
                PR.UpdateRecourceCounter();
            }
        }
    }
    void Attack()
    {
        IsAttacking = true;
        CanAttack = false;
        //instantiate()
        //anim.speed = Pause.Acceleration;
        StartCoroutine(ResetAttackCooldown());
    }
    
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
        IsAttacking = false;
    }
}
