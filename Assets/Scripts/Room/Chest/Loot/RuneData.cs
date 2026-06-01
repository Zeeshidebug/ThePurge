using UnityEngine;
public enum RuneType
{
    Strength,
    Durability,
    Accuracy,
    Power,
    Agility,
    Defense
}

[CreateAssetMenu(
    fileName = "New Rune",
    menuName = "The Purge/Rune"
)]

public class RuneData
: ScriptableObject
{
    public string runeName;

    public RuneType runeType;

    public float value;

    [TextArea]
    public string description;

    public Sprite icon;
}