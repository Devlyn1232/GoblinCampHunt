using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alerts : MonoBehaviour
{
    public GameStates GS;
    public Text T;


    // Update is called once per frame
    void Awake()
{
    GameObject gsObject = GameObject.Find("GameStates");
    if (gsObject != null)
    {
        GS = gsObject.GetComponent<GameStates>();
    }
    else
    {
        Debug.LogError("GameStates object not found in the scene. Game WILL not work woithout GameStates GameObject");
    }
}
    void Start()
    {
        T.enabled = false;
    }
    void Update()
    {
        if(GS.inCombat)
        {
            T.enabled = true;
        }
        else
        {
            T.enabled = false;
        }
    }
}
