using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon Data")]


public class WeaponData : ScriptableObject
{

    [Header("Visual")]
    public Sprite weaponSprite;

    [Header("Basic Info")]
    public string weaponName;

    [TextArea]
    public string description;

    [Header("Weapon Identity")]
    public WeaponCategory weaponCategory;
    public WeaponType weaponType;

    [Header("Combat Stats")]
    public int damage;
    public float attackSpeed;

    [Header("Ranged Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed;

    [Header("Melee Settings")]
    public float slashRadius;
    public float slashAngle;

    // AUTO PROPERTIES

    public bool IsAutoAttack =>
        weaponCategory == WeaponCategory.Light ||
        weaponCategory == WeaponCategory.Dual;

    public bool IsChargeAttack =>
        weaponCategory == WeaponCategory.Heavy;

    public bool OccupyTwoSlots =>
        weaponCategory == WeaponCategory.Dual;
}