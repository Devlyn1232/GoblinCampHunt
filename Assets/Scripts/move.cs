using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3(1, 0, 0); // Default force direction
    public float forceMagnitude = 10.0f; // Adjust the force magnitude as needed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Apply a force in the specified direction when the script starts
        rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);
    }
}
