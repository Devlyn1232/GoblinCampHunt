using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 100;
    public float currentMana;

    [SerializeField] PlayerManaBar ManaBar;
    void Awake()
    {
        ManaBar = GetComponentInChildren<PlayerManaBar>();
    }
    void Start()
    {
        //currentHealth = maxHealth;
        ManaBar.UpdateManaBar(currentMana, maxMana);
    }
    
    void FixedUpdate()
    {
        if(currentMana <= maxMana)
        {
            currentMana += .05f;
            ManaBar.UpdateManaBar(currentMana, maxMana);
        }
    }
    
    // Update is called once per frame
    public bool TakeMana(float manaCost)
    {
        if (currentMana >= manaCost)
        {
            currentMana -= manaCost;
            ManaBar.UpdateManaBar(currentMana, maxMana);
            return true;
        }
        return false;
    }
}