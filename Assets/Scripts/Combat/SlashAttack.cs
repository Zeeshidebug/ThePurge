using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackPoint;


    [Header("Effect")]
    [SerializeField] private GameObject slashEffectPrefab;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField]
    private WeaponData debugWeapon;
    public HitData PerformAttack(WeaponData weapon, DamageData damageData)
    {
        return Slash(weapon, damageData);
    }

    private HitData Slash(WeaponData weapon, DamageData damageData)
    {
        HitData hitData = null;
        bool hitSuccess = false;
        bool hitEnemy = false;

        GameObject slashEffect =
            Instantiate(
                weapon.slashEffectPrefab,
                attackPoint.position,
                attackPoint.rotation
            );

        SlashEffect effect =
            slashEffect.GetComponent<SlashEffect>();

        effect.Initialize(weapon);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            weapon.slashRadius,
            enemyLayer
        );

        Vector2 slashDirection =
            ((Vector2)attackPoint.position - (Vector2)transform.position).normalized;

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 directionToEnemy =
                ((Vector2)enemy.transform.position - (Vector2)attackPoint.position).normalized;

            float angleToEnemy =
                Vector2.Angle(slashDirection, directionToEnemy);

            if (angleToEnemy <= weapon.slashAngle / 2f)
            {
                hitData = new HitData();

                hitData.damageData =
                    damageData;

                hitData.hitPosition =
                    enemy.transform.position;

                EnemyCombat enemyCombat =
                enemy.GetComponent<EnemyCombat>();

                BossCombat bossCombat =
                enemy.GetComponent<BossCombat>();

                float distanceToEnemy =
                Vector2.Distance(
                    attackPoint.position,
                    enemy.transform.position
                );

                RaycastHit2D wallHit =
                Physics2D.Raycast(
                    attackPoint.position,

                    directionToEnemy,

                    distanceToEnemy,

                    wallLayer
                );

                if (
                    wallHit.collider
                    != null
                )
                {
                    continue;
                }

                if (enemyCombat != null)
                {
                    enemyCombat.TakeHit(
                        hitData
                    );

                    hitSuccess = true;
                }

                if (bossCombat != null)
                {
                    bossCombat.TakeDamage(
                        damageData
                    );

                    hitSuccess = true;
                }

                GameObject hitText = Instantiate(
                    CombatVisualManager
                    .Instance
                    .GetHitTextPrefab(),
                    enemy.transform.position + new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.3f, 0.3f),
                    0f
                    ),
                    Quaternion.identity
                );
                hitText.GetComponent<HitText>().SetDamageText(
                        damageData
                    );
                Debug.Log("Enemy Hit!");


            }
        }
        if (hitSuccess)
        {
            return hitData;
        }

        return null;

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        PlayerInventory inventory =
            GetComponent<PlayerInventory>();

        if (inventory == null)
            return;

        WeaponData weapon =
            inventory.slot1.equippedWeapon;

        if (weapon == null)
            return;

        Gizmos.color = Color.red;

        // Radius
        Gizmos.DrawWireSphere(attackPoint.position, debugWeapon.slashRadius);

        Vector2 slashDirection =
            ((Vector2)attackPoint.position - (Vector2)transform.position).normalized;

        Quaternion leftRayRotation =
            Quaternion.Euler(0, 0, debugWeapon.slashAngle / 2);

        Quaternion rightRayRotation =
            Quaternion.Euler(0, 0, -debugWeapon.slashAngle / 2);

        Vector2 leftRayDirection = leftRayRotation * slashDirection;
        Vector2 rightRayDirection = rightRayRotation * slashDirection;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            attackPoint.position,
            (Vector2)attackPoint.position + leftRayDirection * debugWeapon.slashRadius
        );

        Gizmos.DrawLine(
            attackPoint.position,
            (Vector2)attackPoint.position + rightRayDirection * debugWeapon.slashRadius
        );
    }
}