using UnityEngine;

public class BossPunchAttack
: MonoBehaviour
{
    private BossCombat
    bossCombat;

    private Transform
    player;

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
        if (
            player == null
        )
            return;

        float distance =
        Vector2.Distance(
            transform.position,
            player.position
        );

        if (
            distance <=
            bossCombat
            .GetBossData()
            .meleeRange
        )
        {
            PlayerStats
            playerStats =
            player.GetComponent<
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
                    .contactDamage
                );
            }
        }
    }
}