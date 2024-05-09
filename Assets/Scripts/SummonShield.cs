using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonShield : MonoBehaviour
{
    public GameObject ManaShield;
    public float ManaCost = 20;
    [SerializeField] PlayerMana playerMana;

    void Awake()
    {
        playerMana = GetComponentInChildren<PlayerMana>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if(playerMana.TakeMana(ManaCost))
            {
                Instantiate(ManaShield, transform.position, transform.rotation);
            }
        }
    }
}
