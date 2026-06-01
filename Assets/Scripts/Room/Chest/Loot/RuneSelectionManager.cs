using UnityEngine;
using System.Collections.Generic;
public class RuneSelectionManager
: MonoBehaviour
{
    [SerializeField]
    private RuneDatabase
    runeDatabase;

    [SerializeField]
    private GameObject
    runeSelectionPanel;

    [SerializeField]
    private RuneSelectionSlot[]
    runeSlots;

    public static
    RuneSelectionManager
    Instance;

    private void Start()
    {
        runeSelectionPanel
        .SetActive(
            false
        );
    }

    private void Awake()
    {
        Instance = this;
    }

    public RuneData[]
GetRandomRunes(
    int count
)
    {
        List<RuneData>
        availableRunes =
        new List<RuneData>(
            runeDatabase.runePool
        );

        RuneData[] selected =
            new RuneData[count];

        for (
            int i = 0;
            i < count;
            i++
        )
        {
            int randomIndex =
            Random.Range(
                0,
                availableRunes.Count
            );

            selected[i] =
            availableRunes[
                randomIndex
            ];

            availableRunes.RemoveAt(
                randomIndex
            );
        }

        return selected;
    }

    public void DebugRoll()
    {
        RuneData[] runes =
            GetRandomRunes(
                3
            );

        foreach (
            RuneData rune
            in runes
        )
        {
            Debug.Log(
                rune.runeName
            );
        }
    }

    public void OpenRuneSelection()
    {
        RuneData[] runes =
            GetRandomRunes(
                3
            );

        for (
            int i = 0;
            i < runes.Length;
            i++
        )
        {
            runeSlots[i]
            .Setup(
                runes[i]
            );
        }

        runeSelectionPanel
        .SetActive(
            true
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.UI
        );
    }

    private void ApplyRune(
    RuneData rune
)
    {
        switch (
            rune.runeType
        )
        {
            case RuneType.Strength:

                PlayerStats
                .Instance
                .AddDamageModifier(
                    rune.value
                );

                break;

            case RuneType.Durability:

                PlayerStats
                .Instance
                .AddHealthModifier(
                    rune.value
                );

                break;

            case RuneType.Accuracy:

                PlayerStats
                .Instance
                .AddCritChanceModifier(
                    rune.value
                );

                break;

            case RuneType.Power:

                PlayerStats
                .Instance
                .AddCritDamageModifier(
                    rune.value
                );

                break;

            case RuneType.Agility:

                PlayerStats
                .Instance
                .AddSpeedModifier(
                    rune.value
                );

                break;

            case RuneType.Defense:

                PlayerStats
                .Instance
                .AddDefenseModifier(
                    rune.value
                );

                break;

        }

        Debug.Log(
            "Rune Applied: "
            +
            rune.runeName
        );

        Debug.Log(
            "Current Player hp: "
            +
            PlayerStats.Instance.GetMaxHealth()
        );
    }

    public void SelectRune(
        RuneData rune
    )
    {
        ApplyRune(
            rune
        );

        runeSelectionPanel
        .SetActive(
            false
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.Gameplay
        );
    }


}