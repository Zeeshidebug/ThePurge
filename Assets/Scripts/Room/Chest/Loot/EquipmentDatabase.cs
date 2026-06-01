using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Equipment Database",
    menuName = "The Purge/Equipment Database"
)]
public class EquipmentDatabase
: ScriptableObject
{
    public List<EquipmentData>
    equipmentPool =
    new();
}