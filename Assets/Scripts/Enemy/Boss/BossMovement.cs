using UnityEngine;

public class BossMovement
: MonoBehaviour
{
    private Transform player;

    private BossCombat bossCombat;

    private Rigidbody2D rb;

    private float moveSpeed;

    private BossAttackController bossAttackController;

    private void Awake()
    {
        rb =
            GetComponent<Rigidbody2D>();

        bossCombat =
            GetComponent<
                BossCombat
            >();

        bossAttackController =
        GetComponent<
            BossAttackController
        >();
    }

    private void Start()
    {
        GameObject playerObject =
        GameObject
        .FindGameObjectWithTag(
            "Player"
        );

        if (
            playerObject != null
        )
        {
            player =
            playerObject.transform;
        }

        moveSpeed =
            bossCombat
            .GetBossData()
            .moveSpeed;
    }

    private void FixedUpdate()
    {

        if (
            bossAttackController
            .IsAttacking()
        )
        {
            return;
        }

        if (
            player == null
        )
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