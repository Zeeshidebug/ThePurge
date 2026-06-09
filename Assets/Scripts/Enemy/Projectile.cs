using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Fungsi ini akan dipanggil oleh musuh saat melahirkan bola sihir
    public void Launch(Vector2 direction, float projectileSpeed, float projectileDamage)
    {
        moveDirection = direction.normalized;
        speed = projectileSpeed;
        damage = projectileDamage;

        // Atur rotasi agar gambar bola sihir menghadap ke arah terbangnya
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Terbangkan menggunakan Rigidbody
        rb.linearVelocity = moveDirection * speed;

        // Hancurkan otomatis dalam 5 detik jika tidak kena apa-apa (biar tidak bikin lag)
        Destroy(gameObject, 5f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika menabrak Player
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }

            // Jalankan efek ledakan sihir di sini jika ada, lalu hancurkan peluru
            Destroy(gameObject);
        }
        // Jika menabrak dinding/obstacle di dungeon
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}