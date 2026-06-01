using UnityEngine;

public class BossSpikeAttack
: MonoBehaviour
{
    private BossCombat bossCombat;

    private Transform player;

    private void Awake()
    {
        bossCombat =
        GetComponent<
            BossCombat
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
    }

    public void Attack()
    {
        if (player == null)
            return;

        float radius =
        bossCombat
        .GetBossData()
        .spikeRadius;

        Collider2D hit =
        Physics2D.OverlapCircle(
            player.position,
            radius,
            LayerMask.GetMask(
                "Player"
            )
        );

        if (hit != null)
        {
            PlayerStats
            playerStats =
            hit.GetComponent<
                PlayerStats
            >();

            if (
                playerStats != null
            )
            {
                playerStats
                .TakeDamage(
                    bossCombat
                    .GetBossData()
                    .spikeDamage
                );
            }
        }

        Debug.Log(
            "SPIKE ATTACK 😈"
        );
    }
}