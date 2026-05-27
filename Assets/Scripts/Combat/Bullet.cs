using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float lifeTime;

    private Vector2 direction;
    private DamageData damageData;

    [SerializeField] private LayerMask enemyLayer;

    public void Initialize(
        Vector2 dir,
        WeaponData weapon,
        DamageData data
    )
    {
        direction =
            dir.normalized;

        speed =
            weapon.projectileSpeed;

        lifeTime =
            weapon.projectileLifetime;

        damageData =
            data;

        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            ) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(
                0,
                0,
                angle + 225f
            );
    }

    private void Start()
    {
        Destroy(
            gameObject,
            lifeTime
        );
    }

    private void Update()
    {
        transform.position +=
            (Vector3)(
                direction *
                speed *
                Time.deltaTime
            );
    }

    private void OnTriggerEnter2D(
    Collider2D collision
)
    {
        if (
            ((1 << collision.gameObject.layer)
            & enemyLayer)
            == 0
        )
            return;

        HitData hitData =
            new HitData();

        hitData.damageData =
            damageData;

        hitData.hitPosition =
            collision.transform.position;

        EnemyCombat enemyCombat =
            collision.GetComponent<EnemyCombat>();

        if (enemyCombat != null)
        {
            enemyCombat.TakeHit(
                hitData
            );
        }

        GameObject hitText =
            Instantiate(
                CombatVisualManager
                .Instance
                .GetHitTextPrefab(),
                transform.position,
                Quaternion.identity
            );

        hitText
        .GetComponent<HitText>()
        .SetDamageText(
            damageData
        );

        TriggerFeedback();

        Destroy(gameObject);
    }

    private void TriggerFeedback()
    {
        CombatFeedbackData data =
            new CombatFeedbackData();

        data.shakeDuration = 0.05f;
        data.shakeStrength = 0.05f;

        CameraShake.Instance
            .Shake(
                data.shakeDuration,
                data.shakeStrength
            );
    }
}