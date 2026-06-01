using UnityEngine;

public enum EnemyAttackType
{
    Melee,
    Ranged,
    Boss
}

[CreateAssetMenu(
    menuName = "The Purge/Enemy Data"
)]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string enemyName;

    [Header("Stats")]
    public float maxHealth = 100f;

    public float moveSpeed = 2f;

    public float damage = 10f;

    [Header("Rewards")]
    public int soulReward = 5;

    [Header("Attack")]

    public float attackDamage = 10f;

    public float attackRange = 1.5f;

    public float attackCooldown = 1f;

    public float attackWindup = .5f;

    [Header("Ranged")]

    public GameObject
    projectilePrefab;

    public float
    projectileSpeed = 10f;

    public EnemyAttackType attackType;


}