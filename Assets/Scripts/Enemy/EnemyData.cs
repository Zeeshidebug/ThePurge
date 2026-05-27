using UnityEngine;

[CreateAssetMenu(
    menuName = "Enemy/Enemy Data"
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
}