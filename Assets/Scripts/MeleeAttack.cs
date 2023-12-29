using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCombat P;
    public Animator anim;
    public bool IsAttacking;
    public bool CanAttack = true;
    public float attackCooldown = 1f; 
    public float Damage;
    void Start()
    {
        anim = GetComponent<Animator>();
        P = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                Attack();
            }
        }
    }
    void Attack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
        //anim.speed = Pause.Acceleration;
        StartCoroutine(ResetAttackCooldown());
    }
    
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
        IsAttacking = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        //print("ActavatedCollider");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //print("FoundEnemy");
            if (IsAttacking)
            {
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                //print("GotComponent");
                if (enemyHealth != null)
                {
                    //print("DamageEnemy");
                    enemyHealth.TakeDamage(Damage);
                }
            } 
        }
    }
}
