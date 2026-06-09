using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack
: MonoBehaviour
{
    [SerializeField]
    private GameObject
    attackCuePrefab;

    private EnemyCombat
    enemyCombat;

    private Transform player;

    private bool canAttack = true;

    private void Awake()
    {
        enemyCombat =
        GetComponent<
            EnemyCombat
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
            !canAttack
        )
            return;

        StartCoroutine(
            AttackRoutine()
        );
    }

    private IEnumerator
    AttackRoutine()
    {
        canAttack = false;

        EnemyData data =
        enemyCombat
        .GetEnemyData();

        Instantiate(
            attackCuePrefab,
            transform.position,
            Quaternion.identity
        );

        //animasi serangan sini yak 

        yield return
        new WaitForSeconds(
            data.attackWindup
        );

        if (
            player != null
        )
        {
            float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

            if (
                distance <=
                data.attackRange
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
                        data.attackDamage
                    );
                }
            }
        }

        yield return
        new WaitForSeconds(
            data.attackCooldown
        );

        canAttack = true;
    }
}