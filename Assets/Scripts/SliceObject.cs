using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public LayerMask ignoredLayers; // Layers to be ignored by the boxcast
    public GameObject CutDirection;

    public Material crossSectionMaterial;
    public float cutForce = 200;
    public Vector3 boxCastSize = new Vector3(0.5f, 0.5f, 10f); // Size of the boxcast volume
    public float rotationSpeed = 50f; // Rotation speed around Z-axis

    private bool isSlicing = false;
    private float previousMouseX;

    void Update()
    {
        // Check for the 'Q' key press and release
        if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            isSlicing = true;
            previousMouseX = Mouse.current.position.ReadValue().x;
        }
        else if (Keyboard.current[Key.Q].wasReleasedThisFrame)
        {
            isSlicing = false;
        }

        // Rotate the plane around Z-axis based on mouse movement while 'Q' key is held down
        if (isSlicing)
        {
            CutDirection.SetActive(true);
            RotatePlaneWithMouse();
        }

        // Slicing logic when 'Q' key is released
        if (!isSlicing && Keyboard.current[Key.Q].wasReleasedThisFrame)
        {
            CutDirection.SetActive(false);
            PerformSlice();
        }
    }

    void RotatePlaneWithMouse()
    {
        float mouseX = Mouse.current.position.ReadValue().x;
        float mouseXDelta = mouseX - previousMouseX;
        float rotationAmount = mouseXDelta * rotationSpeed * Time.deltaTime;

        planeDebug.Rotate(Vector3.forward, rotationAmount);
        previousMouseX = mouseX;
    }

    void PerformSlice()
    {
        Vector3 sliceDirection = endSlicePoint.position - startSlicePoint.position;

        RaycastHit[] hits = Physics.BoxCastAll(
            startSlicePoint.position,
            boxCastSize * 0.5f,
            sliceDirection.normalized,
            Quaternion.identity,
            sliceDirection.magnitude,
            sliceableLayer,
            QueryTriggerInteraction.Ignore
        );

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (((1 << hit.collider.gameObject.layer) & ignoredLayers) == 0)
                {
                    GameObject target = hit.collider.gameObject;

                    // Store the children before slicing
                    List<Transform> children = new List<Transform>();
                    foreach (Transform child in target.transform)
                    {
                        children.Add(child);
                    }

                    // Perform slicing using world space coordinates
                    SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

                    if (hull != null)
                    {
                        // Create upper and lower hulls
                        GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
                        GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

                        // Set up sliced components for upper and lower hulls
                        SetupSlicedComponent(upperHull);
                        SetupSlicedComponent(lowerHull);

                        // Place the upper and lower hulls at the original object's position
                        upperHull.transform.position = target.transform.position;
                        upperHull.transform.rotation = target.transform.rotation;
                        upperHull.transform.localScale = target.transform.localScale;
                        lowerHull.transform.position = target.transform.position;
                        lowerHull.transform.rotation = target.transform.rotation;
                        lowerHull.transform.localScale = target.transform.localScale;

                        // Destroy the original object after slicing
                        Destroy(target);

                        // Place the children under the upper and lower hulls
                        foreach (Transform child in children)
                        {
                            child.SetParent(child.position.y > planeDebug.position.y ? upperHull.transform : lowerHull.transform);
                        }
                    }
                }
            }
        }
    }


    void ReparentSlicedParts(GameObject originalObject, GameObject upperHull, GameObject lowerHull, List<Transform> children)
    {
        // Set the parent of the upper and lower hulls to null before setting new parents
        upperHull.transform.parent = null;
        lowerHull.transform.parent = null;

        // Create new parent objects for the upper and lower hulls
        GameObject upperParent = new GameObject("UpperSlicedPart");
        GameObject lowerParent = new GameObject("LowerSlicedPart");

        // Set the parent of the upper and lower hulls to their respective new parent objects
        upperHull.transform.SetParent(upperParent.transform);
        lowerHull.transform.SetParent(lowerParent.transform);

        // Match original object's transformation
        upperParent.transform.position = originalObject.transform.position;
        upperParent.transform.rotation = originalObject.transform.rotation;
        lowerParent.transform.position = originalObject.transform.position;
        lowerParent.transform.rotation = originalObject.transform.rotation;

        // Destroy the original object after slicing
        Destroy(originalObject);

        // Place the children under the new parents
        foreach (Transform child in children)
        {
            if (child != null)
            {
                child.SetParent(upperParent.transform);
            }
        }
    }

    void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
        // Change the layer of the sliced object (Replace 'Slicable' with your desired layer)
        slicedObject.layer = LayerMask.NameToLayer("Slicable");
    }
}
