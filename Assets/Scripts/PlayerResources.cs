using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int wood;
    public int rock;
    public int leaf;
    public int food;
    public GameObject Campfire;
    public GameObject CampfireGhost;
    public Transform CampfirePlacepoint;
    [SerializeField] PlayerResourceCounter woodCounter;
    [SerializeField] PlayerResourceCounter rockCounter;
    [SerializeField] PlayerResourceCounter leafCounter;
    [SerializeField] PlayerResourceCounter foodCounter;
    void Awake()
    {
        woodCounter = GameObject.Find("wCounter").GetComponent<PlayerResourceCounter>();
        rockCounter = GameObject.Find("rCounter").GetComponent<PlayerResourceCounter>();
        leafCounter = GameObject.Find("gCounter").GetComponent<PlayerResourceCounter>();
        foodCounter = GameObject.Find("fCounter").GetComponent<PlayerResourceCounter>();
    }
    // Update is called once per frame
    public void UpdateWoodCounter()
    {
        woodCounter.ChangeCounter(wood);
    }
    public void UpdateRockCounter()
    {
        rockCounter.ChangeCounter(rock);
    }
    public void UpdateLeafCounter()
    {
        leafCounter.ChangeCounter(leaf);
    }
    public void UpdateFoodCounter()
    {
        foodCounter.ChangeCounter(food);
    }
    public void BuildCampFire()
    {
        if (wood >= 10)
        {
            Instantiate(CampfireGhost, CampfirePlacepoint.position, CampfirePlacepoint.rotation);
        }
    } 
}
