 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHeal : MonoBehaviour
{
    [SerializeField] PlayerMana playerMana;
    [SerializeField] PlayerHealth PlayerHealth;
    public float ManaCost = 100f;
    public float HealAmmount = 100f;

    void Awake()
    {
        playerMana = GetComponentInChildren<PlayerMana>();
        PlayerHealth = GetComponentInChildren<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            if(playerMana.TakeMana(ManaCost))
            {
                PlayerHealth.Heal(HealAmmount);
            }
        }
    }
}
