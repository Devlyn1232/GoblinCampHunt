using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingHealth : MonoBehaviour
{
    public GameObject Player;
    public GoblinCamp goblinCamp;
    public int minDurability = 50;
    public int maxDurability = 100;
    public int Durability;
    void Start()
    {
        GameObject goblinCampObject = GameObject.Find("GoblinCamp");
        if (goblinCampObject != null)
        {
            goblinCamp = goblinCampObject.GetComponent<GoblinCamp>();
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        Durability = Random.Range(minDurability, maxDurability);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the triggering GameObject has a specific tag
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Perform actions when the trigger event happens with an object having the specified tag
            Debug.Log("Tree was hit by a weapon!");
            Durability--;
            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
