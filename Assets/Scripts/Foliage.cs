using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foliage : MonoBehaviour
{
    public GameObject Player;
    public PlayerResources playerResources;
    public int minDurability = 2;
    public int maxDurability = 4;
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
            playerResources.leaf += 1;
            playerResources.UpdateRecourceCounter();
            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
