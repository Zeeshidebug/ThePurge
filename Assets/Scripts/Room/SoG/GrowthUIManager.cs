using UnityEngine;
using TMPro;

public class GrowthUIManager
: MonoBehaviour
{
    public static
    GrowthUIManager
    Instance;

    private int damageLevel;
    private int healthLevel;
    private int defenseLevel;
    private int speedLevel;
    private int critChanceLevel;
    private int critDamageLevel;

    [SerializeField]
    private TMP_Text damageText;

    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    private TMP_Text defenseText;

    [SerializeField]
    private TMP_Text speedText;

    [SerializeField]
    private TMP_Text critChanceText;

    [SerializeField]
    private TMP_Text critDamageText;

    [SerializeField]
    private GameObject
    growthPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        growthPanel
        .SetActive(
            false
        );
        RefreshUI();
    }

    public void OpenUI()
    {
        growthPanel
        .SetActive(
            true
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.UI
        );

        Debug.Log(
            "GROWTH UI OPEN 🌱"
        );
    }

    public void CloseUI()
    {
        growthPanel
        .SetActive(
            false
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.Gameplay
        );

        Debug.Log(
            "GROWTH UI CLOSED 🌱"
        );
    }

    private int GetCost(
    int level
)
    {
        return 10 +
        (level * 5);
    }

    private void RefreshUI()
    {
        damageText.text =
            "Cost: "
            + GetCost(
                damageLevel
            );

        healthText.text =
            "Cost: "
            + GetCost(
                healthLevel
            );

        defenseText.text =
            "Cost: "
            + GetCost(
                defenseLevel
            );

        speedText.text =
            "Cost: "
            + GetCost(
                speedLevel
            );

        critChanceText.text =
            "Cost: "
            + GetCost(
                critChanceLevel
            );

        critDamageText.text =
            "Cost: "
            + GetCost(
                critDamageLevel
            );
    }

    public void BuyDamage()
    {
        int cost =
            GetCost(
                damageLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS 😭"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddDamageModifier(
            1
        );

        damageLevel++;

        Debug.Log(
            "DAMAGE UP!"
        );
        RefreshUI();
    }

    public void BuyHealth()
    {
        int cost =
            GetCost(
                healthLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS 😭"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddHealthModifier(
            5
        );

        healthLevel++;

        Debug.Log(
            "HEALTH UP!"
        );
        RefreshUI();
    }

    public void BuyDefense()
    {
        int cost =
            GetCost(
                defenseLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS 😭"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddDefenseModifier(
            1
        );

        defenseLevel++;

        Debug.Log(
            "DEFENSE UP!"
        );
        RefreshUI();
    }

    public void BuySpeed()
    {
        int cost =
            GetCost(
                speedLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS 😭"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddSpeedModifier(
            0.01f
        );

        speedLevel++;

        Debug.Log(
            "SPEED UP!"
        );
        RefreshUI();
    }

    public void BuyCritChance()
    {
        int cost =
            GetCost(
                critChanceLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS 😭"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddCritChanceModifier(
            2
        );

        critChanceLevel++;

        Debug.Log(
            "CRIT CHANCE UP!"
        );
        RefreshUI();
    }

    public void BuyCritDamage()
    {
        int cost =
            GetCost(
                critDamageLevel
            );

        if (
            !PlayerStats
            .Instance
            .SpendSoulFragments(
                cost
            )
        )
        {
            Debug.Log(
                "NOT ENOUGH SOULS"
            );

            return;
        }

        PlayerStats
        .Instance
        .AddCritDamageModifier(
            0.05f
        );

        critDamageLevel++;

        Debug.Log(
            "CRIT DAMAGE UP!"
        );
        RefreshUI();
    }
}