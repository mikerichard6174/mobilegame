using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fallbackMoveSpeed = 5f;
    [SerializeField] private VirtualJoystick virtualJoystick;
    [SerializeField] private PlayerStats playerStats;

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
        var moveSpeed = playerStats != null ? playerStats.CurrentMoveSpeed : fallbackMoveSpeed;
        rb.velocity = moveInput * moveSpeed;
    }
}
