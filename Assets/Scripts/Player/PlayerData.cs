using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerData",
    menuName = "The Purge/Player Data"
)]

public class PlayerData
: ScriptableObject
{
    [Header("Stats")]

    public float maxHealth = 100f;

    public float moveSpeed = 5f;

    public float damageMultiplier = 1f;

    public float critMultiplier = 1f;

    public float attackSpeed = 1f;

    public float defense = 0f;

    public float luck = 1f;

    [Header("Progression")]

    public int soulFragments = 0;
}