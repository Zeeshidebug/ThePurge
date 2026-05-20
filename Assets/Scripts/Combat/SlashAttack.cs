using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackPoint;

    [Header("Hit Text")]
    [SerializeField] private GameObject hitTextPrefab;


    [Header("Effect")]
    [SerializeField] private GameObject slashEffectPrefab;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField]
    private WeaponData debugWeapon;

    public void PerformAttack(WeaponData weapon)
    {
        Slash(weapon);
    }

    private void Slash(WeaponData weapon)
    {
        Instantiate(
            slashEffectPrefab,
            attackPoint.position,
            attackPoint.rotation
        );

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
                HitStopManager.Instance.Stop(0.08f);
                CameraShake.Instance.Shake(0.1f, 0.1f);
                GameObject hitText = Instantiate(
                    hitTextPrefab,
                    enemy.transform.position + new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.3f, 0.3f),
                    0f
                    ),
                    Quaternion.identity
                );
                hitText.GetComponent<HitText>().SetText("Hit!");
                Debug.Log("Enemy Hit!");


            }
        }

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