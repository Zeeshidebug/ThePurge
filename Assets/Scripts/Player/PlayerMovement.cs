using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerStats playerStats;

    [SerializeField]
    private float acceleration = 15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameplay())
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity =
            movement *
            playerStats
            .GetMoveSpeed();

        rb.linearVelocity =
            Vector2.Lerp(
                rb.linearVelocity,
                targetVelocity,
                acceleration *
                Time.fixedDeltaTime
            );
    }
}