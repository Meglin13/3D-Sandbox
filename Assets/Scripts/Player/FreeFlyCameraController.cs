using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class FreeFlyCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    public float maxLookAngle = 80f;

    private InputAction moveInput;
    private InputAction lookInput;

    private bool cursorLocked = true;

    private void Start()
    {
        var player = GetComponent<PlayerInput>();
        moveInput = player.actions["Move"];
        lookInput = player.actions["Look"];
        LockCursor();
    }

    private void Update()
    {
        Vector2 moveDelta = moveInput.ReadValue<Vector2>();
        Vector2 lookDelta = lookInput.ReadValue<Vector2>();

        MoveCamera(moveDelta);
        RotateCamera(lookDelta);
        CheckForEscape();
    }

    private void MoveCamera(Vector2 moveDelta)
    {
        Vector3 move = new Vector3(moveDelta.x, 0f, moveDelta.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);
    }

    private void RotateCamera(Vector2 lookDelta)
    {
        float pitch = -lookDelta.y * lookSpeed * Time.deltaTime;
        float yaw = lookDelta.x * lookSpeed * Time.deltaTime;

        // Pitch (вращение вокруг оси X)
        transform.rotation *= Quaternion.AngleAxis(pitch, Vector3.right);

        // Yaw (вращение вокруг оси Y)
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + yaw,
            transform.eulerAngles.z
        );
    }
    private void LockCursor()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }

    private void CheckForEscape()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            cursorLocked = !cursorLocked;
            LockCursor();
        }
    }
}
