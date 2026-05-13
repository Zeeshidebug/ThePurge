using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime;

    private void Update()
    {

    }

    public void PerformAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        Vector2 direction = (attackPoint.position - player.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}