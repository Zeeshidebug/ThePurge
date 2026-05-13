using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackPoint;

    [Header("Hit Text")]
    [SerializeField] private GameObject hitTextPrefab;

    [Header("Slash Settings")]
    [SerializeField] private float slashRadius = 1.5f;
    [SerializeField] private float slashAngle = 90f;

    [Header("Effect")]
    [SerializeField] private GameObject slashEffectPrefab;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    private void Update()
    {

    }

    public void PerformAttack()
    {
        Slash();
    }

    private void Slash()
    {
        Instantiate(
            slashEffectPrefab,
            attackPoint.position,
            attackPoint.rotation
        );

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            slashRadius,
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

            if (angleToEnemy <= slashAngle / 2f)
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
        if (attackPoint == null) return;

        Gizmos.color = Color.red;

        // Radius
        Gizmos.DrawWireSphere(attackPoint.position, slashRadius);

        Vector2 slashDirection =
            ((Vector2)attackPoint.position - (Vector2)transform.position).normalized;

        Quaternion leftRayRotation =
            Quaternion.Euler(0, 0, slashAngle / 2);

        Quaternion rightRayRotation =
            Quaternion.Euler(0, 0, -slashAngle / 2);

        Vector2 leftRayDirection = leftRayRotation * slashDirection;
        Vector2 rightRayDirection = rightRayRotation * slashDirection;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            attackPoint.position,
            (Vector2)attackPoint.position + leftRayDirection * slashRadius
        );

        Gizmos.DrawLine(
            attackPoint.position,
            (Vector2)attackPoint.position + rightRayDirection * slashRadius
        );
    }
}