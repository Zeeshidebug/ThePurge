using UnityEngine;

public class EnemyAttackController
: MonoBehaviour
{
    [SerializeField]
    private EnemyMeleeAttack
    meleeAttack;

    [SerializeField]
    private EnemyRangedAttack
    rangedAttack;

    private EnemyCombat
    enemyCombat;

    private Transform player;

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

    private void Update()
    {
        if (
            player == null
        )
            return;

        EnemyData data =
        enemyCombat
        .GetEnemyData();

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
            Attack();
        }
    }

    public void Attack()
    {
        switch (
            enemyCombat
            .GetEnemyData()
            .attackType
        )
        {
            case EnemyAttackType.Melee:

                meleeAttack
                .Attack();

                break;

            case EnemyAttackType.Ranged:

                rangedAttack
                .Attack();

                break;
        }
    }
}