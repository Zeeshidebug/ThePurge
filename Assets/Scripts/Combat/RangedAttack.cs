using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform player;

    public void PerformAttack(WeaponData weapon, DamageData damageData)
    {
        Vector2 direction =
            (attackPoint.position - player.position)
            .normalized;

        GameObject bullet =
            Instantiate(
                weapon.projectilePrefab,
                attackPoint.position,
                Quaternion.identity
            );

        Bullet bulletScript =
            bullet.GetComponent<Bullet>();

        bulletScript.Initialize(
            direction,
            weapon,
            damageData
        );
    }
}