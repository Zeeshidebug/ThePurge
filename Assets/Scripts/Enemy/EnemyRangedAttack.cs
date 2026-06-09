using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackCuePrefab;

    private EnemyCombat enemyCombat;
    private Animator anim; // 1. Tambahkan variabel untuk menampung Animator
    private Transform player;
    private bool canAttack = true;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        anim = GetComponent<Animator>(); // 2. Ambil komponen Animator dari objek ini saat game mulai
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void Attack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;

        EnemyData data = enemyCombat.GetEnemyData();

        if (attackCuePrefab != null)
        {
            Instantiate(attackCuePrefab, transform.position, Quaternion.identity);
        }

        // [DIPINDAHKAN] anim.SetTrigger("Attack" ) dicabut dari sini agar tidak curi start duluan

        // Tunggu jeda ancang-ancang (windup data)
        yield return new WaitForSeconds(data.attackWindup);

        if (player != null && data.projectilePrefab != null)
        {
            // DI SINI TEMPATNYA: Jalankan animasi TEPAT bersamaan dengan munculnya peluru & damage!
            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }

            Vector2 direction = (player.position - transform.position).normalized;

            // Memunculkan peluru dengan offset 0.5 unit di depan musuh agar rapi
            GameObject projectile = Instantiate(
                data.projectilePrefab,
                transform.position + (Vector3)(direction * 0.5f),
                Quaternion.identity
            );

            Bullet bullet = projectile.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetOwner(gameObject);

                // Membuat data damage
                DamageData damageData = new DamageData();
                damageData.damage = data.attackDamage; 
                damageData.chargeMultiplier = 1f;
                
                // Jalankan peluru
                bullet.Initialize(direction, data.projectileSpeed, 5f, damageData);
            }
        }

        yield return new WaitForSeconds(data.attackCooldown);
        canAttack = true;
    }
}