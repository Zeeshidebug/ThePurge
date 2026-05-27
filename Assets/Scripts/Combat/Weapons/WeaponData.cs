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
    public float critChance = 0f;
    public float critMultiplier = 2f;

    [Header("Status Effect")]

    public StatusEffectType statusEffect;

    public float effectChance;

    public float effectDuration;

    public float effectDamage;

    [Header("Ranged Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileLifetime;

    [Header("Melee Settings")]
    public float slashRadius;
    public float slashAngle;
    public GameObject slashEffectPrefab;
    public float slashEffectDuration;

    [Header("Charge Settings")]
    public float minChargeTime = 0.3f;
    public float maxChargeTime = 1.5f;
    public float minChargeMultiplier = 1f;
    public float maxChargeMultiplier = 3f;

    public bool IsAutoAttack =>
        weaponCategory == WeaponCategory.Light ||
        weaponCategory == WeaponCategory.Dual;

    public bool IsChargeAttack =>
        weaponCategory == WeaponCategory.Heavy;

    public bool OccupyTwoSlots =>
        weaponCategory == WeaponCategory.Dual;
}