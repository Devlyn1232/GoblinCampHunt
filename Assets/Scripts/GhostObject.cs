using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuildables : MonoBehaviour
{
    public GameObject PlacedObject;
    [SerializeField] PlayerResources PR;
    public int WoodCost;
    public int StoneCost;
    public int GrassCost;
    public int FoodCost;
    void Awake()
    {
        PR = GameObject.Find("Player").GetComponent<PlayerResources>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            PR.wood -= WoodCost;
            PR.rock -= StoneCost;
            PR.leaf -= GrassCost;
            PR.food -= FoodCost;
            PR.UpdateRecourceCounter();
            Instantiate(PlacedObject, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
