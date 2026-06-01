using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Loot Database",
    menuName = "The Purge/Loot Database"
)]
public class LootDatabase
: ScriptableObject
{
    public List<LootData>
    lootPool =
    new List<LootData>();
}