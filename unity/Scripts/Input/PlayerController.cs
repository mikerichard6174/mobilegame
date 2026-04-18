using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private VirtualJoystick virtualJoystick;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            moveInput = Vector2.zero;
            return;
        }

        var joystickInput = virtualJoystick != null ? virtualJoystick.InputVector : Vector2.zero;
        if (joystickInput.sqrMagnitude > 0f)
        {
            moveInput = joystickInput;
            return;
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}
