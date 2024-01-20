using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCampFire : MonoBehaviour
{
    public GameObject Campfire;
    [SerializeField] PlayerResources PR;
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
            PR.wood -= 10;
            PR.UpdateWoodCounter();
            Instantiate(Campfire, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
