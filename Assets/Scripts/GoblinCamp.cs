using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCamp : MonoBehaviour
{
    [HideInInspector] public int currentGoblins;
    public int MaxGoblins;
    public GameObject Goblin;
    public GameObject[] goblinHouses;
    public float WanderRange = 100;
    //public bool CanSummonGoblin;
    
    // Update is called once per frame
    void Start()
    {
        currentGoblins = 0;
        StartCoroutine(SummonGoblinAtCamp());
    }
    void FixedUpdate()
    {
        if(goblinHouses == null)
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator SummonGoblinAtCamp()
    {
        if(currentGoblins < MaxGoblins)
        {
            Instantiate(Goblin, transform.position, transform.rotation);
            currentGoblins ++;
        }
        yield return new WaitForSeconds(Random.Range(10, 20));
        StartCoroutine(SummonGoblinAtCamp());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawWireSphere(transform.position, WanderRange);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nature"))
        {
            Objects objects = other.GetComponent<Objects>();
            if (objects != null)
            {
                objects.CollideWithObsticle();
            }
        }
    }
}
