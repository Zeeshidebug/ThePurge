using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerStats playerStats;

    [SerializeField]
    private float acceleration = 15f;

    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameplay())
        {
            movement = Vector2.zero;
            if (animator != null) animator.SetFloat("speed", 0);
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
        
        if (animator != null)
        {
            animator.SetFloat("speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movement * playerStats.GetMoveSpeed();

        rb.linearVelocity = Vector2.Lerp(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }
}