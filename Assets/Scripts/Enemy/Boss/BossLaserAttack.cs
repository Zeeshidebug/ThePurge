using UnityEngine;

public class BossLaserAttack
: MonoBehaviour
{
    [SerializeField]
    private GameObject
    projectilePrefab;

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

        Vector2 direction =
        (
            player.position
            -
            transform.position
        ).normalized;

        GameObject projectile =
        Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Bullet bullet =
        projectile.GetComponent<
            Bullet
        >();

        BossData data =
        bossCombat
        .GetBossData();

        DamageData damageData =
        new DamageData();

        damageData.damage =
            data.laserDamage;

        damageData.chargeMultiplier =
            1f;

        bullet.Initialize(
            direction,
            data.projectileSpeed,
            data.projectileLifetime,
            damageData
        );

        bullet.SetOwner(
            gameObject
        );
    }
}