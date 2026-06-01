using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyCombat : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField]
    private EnemyData enemyData;

    private float currentHealth;


    private Dictionary<
        StatusEffectType,
        Coroutine
    > activeEffects
    =
    new Dictionary<
        StatusEffectType,
        Coroutine
    >();

    private Dictionary<
        StatusEffectType,
        float
    > effectEndTimes
    =
    new Dictionary<
        StatusEffectType,
        float
    >();
    private void Start()
    {
        currentHealth =
        enemyData.maxHealth;
    }

    public void TakeHit(
     HitData hitData
 )
    {
        float damage =
            hitData.damageData
            .FinalDamage();

        currentHealth -= damage;

        if (
            hitData.damageData.statusEffect
            !=
            StatusEffectType.None
        )
        {
            ApplyStatusEffect(
                hitData.damageData
            );
        }

        Debug.Log(
            gameObject.name +
            " HP: " +
            currentHealth
        );

        CheckDeath();
    }

    private void ApplyStatusEffect(
    DamageData damageData
)
    {
        StatusEffectType effect =
            damageData.statusEffect;

        if (
            effect ==
            StatusEffectType.None
        )
            return;

        effectEndTimes[effect] =
            Time.time +
            damageData.effectDuration;

        if (
            activeEffects.ContainsKey(
                effect
            )
        )
        {
            return;
        }

        Coroutine routine = null;

        switch (effect)
        {
            case StatusEffectType.Burn:

                routine =
                    StartCoroutine(
                        BurnEffect(
                            damageData
                        )
                    );

                break;

            case StatusEffectType.Bleed:

                routine =
                    StartCoroutine(
                        BleedEffect(
                            damageData
                        )
                    );

                break;
        }

        activeEffects.Add(
            effect,
            routine
        );
    }

    private IEnumerator BurnEffect(
    DamageData damageData
)
    {
        while (
            Time.time <
            effectEndTimes[
                StatusEffectType.Burn
            ]
        )
        {
            yield return
                new WaitForSeconds(
                    1f
                );

            currentHealth -=
                damageData.effectDamage;

            HitText.SpawnStatusText(
                CombatVisualManager
                .Instance
                .GetHitTextPrefab(),

                transform.position,

                "BURN\n" +
                damageData.effectDamage,

                Color.red
            );

            Debug.Log(
                "BURN " +
                damageData.effectDamage
            );

            CheckDeath();

            if (currentHealth <= 0)
                yield break;
        }

        activeEffects.Remove(
            StatusEffectType.Burn
        );

        effectEndTimes.Remove(
            StatusEffectType.Burn
        );
    }

    private IEnumerator BleedEffect(
    DamageData damageData
)
    {
        while (
            Time.time <
            effectEndTimes[
                StatusEffectType.Bleed
            ]
        )
        {
            yield return
                new WaitForSeconds(
                    .5f
                );

            currentHealth -=
                damageData.effectDamage;

            HitText.SpawnStatusText(
                CombatVisualManager
                .Instance
                .GetHitTextPrefab(),

                transform.position,

                "BLEED\n" +
                damageData.effectDamage,

                Color.magenta
            );

            CheckDeath();

            if (currentHealth <= 0)
                yield break;
        }
        activeEffects.Remove(
            StatusEffectType.Bleed
        );

        effectEndTimes.Remove(
            StatusEffectType.Bleed
        );
    }

    private void Die()
    {
        Debug.Log(
            gameObject.name +
            " DIED 😭🔥"
        );

        PlayerStats
        .Instance
        .AddSoulFragments(
            5
        );

        RoomManager
        .Instance
        .EnemyKilled();

        Destroy(gameObject);
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Die();
        }
    }

    public EnemyData GetEnemyData()
    {
        return enemyData;
    }
}