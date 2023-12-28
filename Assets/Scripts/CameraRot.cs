using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if Q key is NOT pressed before allowing camera movement
        if (!Keyboard.current[Key.Q].isPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
