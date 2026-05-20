using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform player;

    public void PerformAttack()
    {
        Vector2 direction =
            (attackPoint.position - player.position)
            .normalized;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                attackPoint.position,
                Quaternion.identity
            );

        bullet
            .GetComponent<Bullet>()
            .SetDirection(direction);
    }
}