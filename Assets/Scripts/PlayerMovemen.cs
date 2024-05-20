using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.1f; // Distance for ground check

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    private const float dashDistance = 40;
    private const float dashDuration = 0.1f;
    private const float dashCooldown = 0.1f;
    public float dashManaCost = 10f;

    public bool isDashing;
    public float dashTimer;
    public float dashCooldownTimer;
    private bool dashOnCooldown;
    private Vector3 dashDirection;
    private Vector3 velocity;
    public LayerMask obstacleMask;
    public LayerMask groundMask; // Layer mask for ground check
    public DashCooldownVisuial DashCooldownV;
    [SerializeField] PlayerMana playerMana;

    private bool isGrounded;

    void Awake()
    {
        playerMana = GetComponentInChildren<PlayerMana>();
    }

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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Check for dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartDash();
        }

        // Check if the player is on the ground
        GroundCheck();
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
        if (playerMana.TakeMana(dashManaCost))
        {
            isDashing = true;
            dashOnCooldown = true;
            DashCooldownV.Cooldown(dashCooldown);
            Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            dashDirection = transform.TransformDirection(inputDirection) * dashDistance;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void Dash()
    {
        dashTimer -= Time.fixedDeltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, dashDistance, obstacleMask))
        {
            // Stop the dash if an obstacle is hit
            isDashing = false;
            velocity = Vector3.zero;
        }
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
        // Ensure the player is grounded before jumping
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Set isGrounded to false immediately after jumping
        }
    }

    void GroundCheck()
    {
        // Perform a raycast downwards to check if the player is on the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
