using UnityEngine;

public class BossCombat
: MonoBehaviour
{
    [SerializeField]
    private BossData
    bossData;

    private float currentHealth;

    private void Start()
    {
        currentHealth =
            bossData.maxHealth;
    }

    public void TakeDamage(
        DamageData damageData
    )
    {
        currentHealth -=
            damageData.FinalDamage();

        Debug.Log(
            "Boss HP: " +
            currentHealth
        );

        CheckDeath();
    }

    private void Die()
    {
        Debug.Log(
            "BOSS DEFEATED 😭🔥"
        );

        Debug.Log(
    "GG, YOU FINISHED THE GAME LMAO 👑"
);

        RoomManager
        .Instance
        .EnemyKilled();

        Destroy(
            gameObject
        );
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Die();
        }
    }

    public BossData GetBossData()
    {
        return bossData;
    }
}