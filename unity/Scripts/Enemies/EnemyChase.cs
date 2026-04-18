using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (player == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        var direction = ((Vector2)(player.position - transform.position)).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
