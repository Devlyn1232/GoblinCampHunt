using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCamp : MonoBehaviour
{
    
    public float WanderRange = 100;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawWireSphere(transform.position, WanderRange);
    }
}
