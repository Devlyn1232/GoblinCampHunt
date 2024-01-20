using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject Player;
    public PlayerResources playerResources;
    public int minDurability = 5;
    public int maxDurability = 10;
    public int Durability;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerResources = Player.GetComponent<PlayerResources>();
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
            playerResources.wood += 1;
            playerResources.UpdateWoodCounter();
            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
