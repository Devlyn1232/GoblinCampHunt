using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    
    // Start is called before the first frame update
    public float DeathTimer;
    void Start()
    {
        Destroy(gameObject, DeathTimer);
    }
}