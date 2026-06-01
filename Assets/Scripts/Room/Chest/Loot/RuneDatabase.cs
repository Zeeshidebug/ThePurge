using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Rune Database",
    menuName = "The Purge/Rune Database"
)]
public class RuneDatabase
: ScriptableObject
{
    public List<RuneData>
    runePool =
    new();
}