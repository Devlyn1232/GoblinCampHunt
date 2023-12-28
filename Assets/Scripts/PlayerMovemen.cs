using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 8f; // Adjust the jump force as needed

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent object from tilting due to physics
    }

    void Update()
    {
        // Get user input for movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Calculate movement
        Vector3 movementV = transform.forward * verticalInput * movementSpeed * Time.fixedDeltaTime;
        Vector3 movementH = transform.right * horizontalInput * movementSpeed * Time.fixedDeltaTime;

        // Apply movement to the Rigidbody
        rb.MovePosition(rb.position + movementV + movementH);
    }

    void Jump()
    {
        // Check if the object is grounded before jumping (You may want to implement a grounded check)
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
