using UnityEngine;

[CreateAssetMenu(
    fileName = "BossData",
    menuName = "The Purge/Boss Data"
)]
public class BossData
: ScriptableObject
{
    [Header("Stats")]

    public float maxHealth = 500f;

    public float moveSpeed = 0.5f;

    public float contactDamage = 20f;

    [Header("Combat")]

    public float attackCooldown = 2f;

    public float meleeRange = 2f;

    public float spikesRange = 5f;
    public float spikeDamage = 15f;
    public float spikeRadius = 1.5f;

    public float laserRange = 10f;
    public float laserDamage = 20f;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;
}