using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Weapon Database",
    menuName = "The Purge/Weapon Database"
)]
public class WeaponDatabase
: ScriptableObject
{
    public List<WeaponData>
    weaponPool =
    new();
}