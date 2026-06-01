using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float lifeTime;

    private Vector2 direction;
    private DamageData damageData;
    private GameObject owner;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask wallLayer;

    public void Initialize(
        Vector2 dir,
        float projectileSpeed,
        float projectileLifetime,
        DamageData data
    )
    {
        direction =
            dir.normalized;

        speed =
            projectileSpeed;

        lifeTime =
            projectileLifetime;

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
                angle + 90f
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
        Debug.Log(
    collision.gameObject.name
);
        if (
            collision.gameObject
            ==
            owner
        )
        {
            return;
        }

        if (
            ((1 << collision.gameObject.layer)
            &
            wallLayer)
            != 0
        )
        {
            Destroy(
                gameObject
            );

            return;
        }

        if (
            ((1 << collision.gameObject.layer)
            & targetLayer)
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

        PlayerStats playerStats =
        collision.GetComponent<PlayerStats>();

        BossCombat bossCombat =
        collision.GetComponent<BossCombat>();

        if (enemyCombat != null)
        {
            enemyCombat.TakeHit(
                hitData
            );
        }

        if (bossCombat != null)
        {
            bossCombat.TakeDamage(
                damageData
            );
        }

        if (playerStats != null)
        {
            playerStats.TakeDamage(
                damageData.FinalDamage()
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

    public void SetOwner(
    GameObject newOwner
)
    {
        owner = newOwner;
    }
}