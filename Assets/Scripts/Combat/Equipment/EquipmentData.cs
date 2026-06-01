using UnityEngine;

[CreateAssetMenu(
    fileName = "New Equipment",
    menuName = "The Purge/Equipment"
)]
public class EquipmentData
: ScriptableObject
{
    public string equipmentName;

    [TextArea]
    public string description;

    public Sprite icon;
}