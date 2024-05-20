using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public GameObject Player;
    public GoblinCamp goblinCamp;
    public LayerMask Nature;
    public int minDurability = 10;
    public int maxDurability = 50;
    public int Durability;
    public GameObject hitParticle;

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
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Building was hit by a weapon!");
            Durability--;
            Instantiate(hitParticle, transform.position, transform.rotation);
            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }

        if ((Nature.value & (1 << other.gameObject.layer)) > 0)
        {
            Objects objects = other.GetComponent<Objects>();
            if (objects != null)
            {
                Destroy(objects);
            }
        }
    }
}
