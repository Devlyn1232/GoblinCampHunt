using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject buildMenu;
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    float verticalRotation = 0f;

    void Start()
    {
        buildMenu = GameObject.Find("BuildMenu");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Keyboard.current != null && buildMenu != null)
        {
            if (!Keyboard.current[Key.Q].isPressed && !buildMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                verticalRotation -= mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
                if (playerBody != null)
                {
                    playerBody.Rotate(Vector3.up * mouseX);
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
