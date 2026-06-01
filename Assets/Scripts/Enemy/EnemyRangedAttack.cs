using UnityEngine;
using System.Collections;

public class EnemyRangedAttack
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

        yield return
        new WaitForSeconds(
            data.attackWindup
        );

        if (
            player != null
        )
        {
            Vector2 direction =
            (
                player.position
                -
                transform.position
            ).normalized;

            GameObject projectile =
            Instantiate(
                data.projectilePrefab,
                transform.position + (Vector3)(direction * .5f),
                Quaternion.identity
            );

            Bullet bullet =
            projectile.GetComponent<
                Bullet
            >();

            bullet.SetOwner(
                gameObject
            );

            DamageData damageData =
            new DamageData();

            damageData.damage =
                data.attackDamage;

            damageData.chargeMultiplier =
                1f;

            bullet.Initialize(
            direction,
            data.projectileSpeed,
            5f,
            damageData
            );

            Rigidbody2D rb =
            projectile
            .GetComponent<
                Rigidbody2D
            >();
        }

        yield return
        new WaitForSeconds(
            data.attackCooldown
        );

        canAttack = true;
    }
}