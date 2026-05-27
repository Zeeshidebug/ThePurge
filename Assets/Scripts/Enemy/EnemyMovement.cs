using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;

    private EnemyCombat enemyCombat;

    private Rigidbody2D rb;

    private float moveSpeed;

    private void Awake()
    {
        rb =
            GetComponent<Rigidbody2D>();

        enemyCombat =
            GetComponent<
                EnemyCombat
            >();
    }

    private void Start()
    {
        moveSpeed =
            enemyCombat
            .GetEnemyData()
            .moveSpeed;

        GameObject playerObject =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (playerObject != null)
        {
            player =
                playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (
            !GameStateManager
            .Instance
            .IsGameplay()
        )
        {
            rb.linearVelocity =
                Vector2.zero;

            return;
        }

        if (player == null)
            return;

        Vector2 direction =
            (
                player.position
                -
                transform.position
            ).normalized;

        rb.linearVelocity =
            direction *
            moveSpeed;
    }
}