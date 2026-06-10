using UnityEngine;

public class PlayerStats
: MonoBehaviour
{
    [SerializeField]
    private PlayerData
    playerData;

    public static
    PlayerStats
    Instance;

    private float currentHealth;
    private int currentSoulFragments;
    private float damageModifier;
    private float critChanceModifier;
    private float critDamageModifier;
    private float defenseModifier;
    private float hpModifier;
    private float speedModifier;

    private void Start()
    {
        currentHealth =
        playerData.maxHealth;

        currentSoulFragments =
      playerData.soulFragments;

        HUDManager
        .Instance
        .UpdateSoulFragments(
            currentSoulFragments
        );

        Debug.Log(
    "PLAYER DATA SOULS: "
    + playerData.soulFragments
);

        Debug.Log(
            playerData.GetInstanceID()
        );
    }

    private void Awake()
    {
        Instance = this;
    }

    public float GetMoveSpeed()
    {
        return playerData.moveSpeed + speedModifier;
    }

    public float GetMaxHealth()
    {
        return playerData.maxHealth + hpModifier;
    }


    public void AddDamageModifier(
    float amount
)
    {
        damageModifier += amount;

        Debug.Log(
            "Damage Modifier: "
            + damageModifier
        );
    }

    public void AddCritChanceModifier(
        float amount
    )
    {
        critChanceModifier += amount;

        Debug.Log(
            "Crit Modifier: "
            + critChanceModifier
        );
    }

    public void AddDefenseModifier(
        float amount
    )
    {
        defenseModifier += amount;

        Debug.Log(
            "Defense Modifier: "
            + defenseModifier
        );
    }

    public void AddCritDamageModifier(
        float amount
    )
    {
        critDamageModifier += amount;

        Debug.Log(
            "Crit Damage Modifier: "
            + critDamageModifier
        );
    }

    public void AddHealthModifier(
    float amount
)
    {
        hpModifier += amount;

        currentHealth += amount;

        Debug.Log(
            "Health Modifier: "
            + hpModifier
        );
    }

    public void AddSpeedModifier(
    float amount)
    {
        speedModifier += amount;

        Debug.Log(
            "Speed Modifier: "
            + speedModifier
        );
    }

    public void AddSoulFragments(
        int amount
    )
    {
        currentSoulFragments += amount;

        playerData.soulFragments =
            currentSoulFragments;

        HUDManager
        .Instance
        .UpdateSoulFragments(
            currentSoulFragments
        );

        Debug.Log(
            "Soul Fragments: "
            + currentSoulFragments
        );
    }

    public float GetDamageModifier()
    {
        return damageModifier;
    }

    public float GetCritChanceModifier()
    {
        return critChanceModifier;
    }

    public float GetDefenseModifier()
    {
        return defenseModifier;
    }

    public float GetCritDamageModifier()
    {
        return critDamageModifier;
    }

    public float GetHealthModifier()
    {
        return hpModifier;
    }

    public float GetSpeedModifier()
    {
        return speedModifier;
    }

    public int GetSoulFragments()
    {
        return currentSoulFragments;
    }

    public void TakeDamage(
        float damage
    )
    {
        float finalDefense =
        playerData.defense + defenseModifier;

        float finalDamage =
        Mathf.Max(
            damage -
            finalDefense,
            1
        );

        currentHealth -=
        finalDamage;

        Debug.Log(
            "Player HP: "
            + currentHealth
        );


        if (
            currentHealth <= 0
        )
        {
            Debug.Log(
                "PLAYER DIED 😭🔥"
            );

            GameOverManager
            .Instance
            .ShowGameOver();
        }
    }

    public bool SpendSoulFragments(
    int amount
)
    {
        if (
            currentSoulFragments <
            amount
        )
        {
            return false;
        }

        currentSoulFragments -= amount;

        playerData.soulFragments =
    currentSoulFragments;

        HUDManager
        .Instance
        .UpdateSoulFragments(
                currentSoulFragments
        );

        return true;
    }

    public void SetSoulFragments(
    int amount
)
    {
        currentSoulFragments =
            amount;

        playerData.soulFragments =
            amount;

        HUDManager
        .Instance
        .UpdateSoulFragments(
            currentSoulFragments
        );
    }


}