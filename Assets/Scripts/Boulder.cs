using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
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
            Debug.Log("Rock was hit by a weapon!");
            Durability--;
            playerResources.rock += 1;
            playerResources.UpdateRockCounter();
            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
