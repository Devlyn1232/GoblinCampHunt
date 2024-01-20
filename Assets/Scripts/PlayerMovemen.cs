using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    private const float dashDistance = 30;
    private const float dashDuration = 0.1f;
    private const float dashCooldown = 1.5f;

    public bool isDashing;
    public float dashTimer;
    public float dashCooldownTimer;
    private bool dashOnCooldown;
    private Vector3 dashDirection;
    private Vector3 velocity;
    public LayerMask obstacleMask;
    public DashCooldownVisuial DashCooldownV;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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

        // Check for dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            Move();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashOnCooldown = true;
        DashCooldownV.Cooldown(dashCooldown);
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        dashDirection = transform.TransformDirection(inputDirection) * dashDistance;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
    }

    private void Dash()
    {
        dashTimer -= Time.fixedDeltaTime;
        if (dashTimer <= 0)
        {
            isDashing = false;
            return;
        }

        // Move the player in the dash direction
        rb.MovePosition(rb.position + dashDirection * Time.fixedDeltaTime);
    }

    private void Move()
    {
        if (dashOnCooldown)
        {
            dashCooldownTimer -= Time.fixedDeltaTime;
            DashCooldownV.SetCooldown(dashCooldownTimer);
            if (dashCooldownTimer <= 0)
            {
                dashOnCooldown = false;
            }
        }
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        // Implement a grounded check here
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
