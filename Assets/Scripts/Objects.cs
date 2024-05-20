using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public bool rotation = true;
    public LayerMask GroundMask;
    public LayerMask Nature;
    void Start()
    {
        Invoke("FindLand", .05f);
        if(rotation)
        {
            transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
    }

    public void FindLand()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, GroundMask))
        {
            transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        }
        else
        {
            ray = new Ray(transform.position, transform.up);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, GroundMask))
            {
                transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the other object is on the Nature layer
        if ((Nature.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }
}
