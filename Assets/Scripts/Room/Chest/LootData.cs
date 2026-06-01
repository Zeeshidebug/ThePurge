using UnityEngine;

[CreateAssetMenu(
    fileName = "New Loot",
    menuName = "The Purge/Loot"
)]
public class LootData
: ScriptableObject
{
    public LootType lootType;

    public int soulFragmentReward;

    public string lootName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Min(1)]
    public int weight = 1;
}