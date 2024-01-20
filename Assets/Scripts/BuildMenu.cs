using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour
{
    public GameObject BuildUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (BuildUI.activeSelf)
            {
                BuildUI.SetActive(false);
            }
            else
            {
                BuildUI.SetActive(true);
            }
            
        }
    }
}
