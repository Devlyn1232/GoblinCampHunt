using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCamp : MonoBehaviour
{
    [HideInInspector] public int currentGoblins;
    public int MaxGoblins;
    public GameObject Goblin;
    public float WanderRange = 100;
    public bool CanSummonGoblin;
    
    // Update is called once per frame
    void Start()
    {
        currentGoblins = 0;
    }
    void Update()
    {
        if(CanSummonGoblin)
        {
            StartCoroutine(SummonGoblinAtCamp());
        }
    }

    public IEnumerator SummonGoblinAtCamp()
    {
        CanSummonGoblin = false;
        yield return new WaitForSeconds(Random.Range(10, 20));
        Instantiate(Goblin, transform.position, transform.rotation);
        CanSummonGoblin = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawWireSphere(transform.position, WanderRange);
    }
}
