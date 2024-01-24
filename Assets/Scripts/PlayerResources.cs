using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int wood;
    public int rock;
    public int leaf;
    public int food;
    public GameObject CampfireGhost;
    public GameObject SnareTrapGhost;
    public GameObject Camoflauge;
    public Transform Placepoint;
    [SerializeField] PlayerResourceCounter woodCounter;
    [SerializeField] PlayerResourceCounter rockCounter;
    [SerializeField] PlayerResourceCounter leafCounter;
    [SerializeField] PlayerResourceCounter foodCounter;
    void Awake()
    {
        UpdateRecourceCounter();
        woodCounter = GameObject.Find("wCounter").GetComponent<PlayerResourceCounter>();
        rockCounter = GameObject.Find("rCounter").GetComponent<PlayerResourceCounter>();
        leafCounter = GameObject.Find("gCounter").GetComponent<PlayerResourceCounter>();
        foodCounter = GameObject.Find("fCounter").GetComponent<PlayerResourceCounter>();
    }
    public void UpdateRecourceCounter()
    {
        foodCounter.ChangeCounter(food);
        leafCounter.ChangeCounter(leaf);
        rockCounter.ChangeCounter(rock);
        woodCounter.ChangeCounter(wood);
    }
    public void BuildCampFire()
    {
        if (wood >= 10)
        {
            Instantiate(CampfireGhost, Placepoint.position, Placepoint.rotation);
        }
    } 
    public void BuildSnareTrap()
    {
        if (wood >= 5 && rock >= 5 && leaf >= 10 && food >= 10)
        {
            Instantiate(SnareTrapGhost, Placepoint.position, Placepoint.rotation);
        }
    } 
    public void CamoflaugeSlef()
    {
        Camoflauge.SetActive(true);
    }
}
