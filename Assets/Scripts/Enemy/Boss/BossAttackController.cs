using UnityEngine;
using System.Collections;

public class BossAttackController
: MonoBehaviour
{
    [SerializeField]
    private BossPunchAttack
    bossPunchAttack;

    [SerializeField]
    private BossSpikeAttack
    bossSpikeAttack;

    [SerializeField]
    private BossLaserAttack
    bossLaserAttack;

    [SerializeField]
    private GameObject attackCuePrefab;

    [SerializeField]
    private float attackWindup = 1f;

    [SerializeField]
    private float backstepDuration = 1f;

    [SerializeField]
    private float backstepSpeed = 4f;

    [SerializeField]
    [Range(0f, 1f)]
    private float backstepChance = 0.25f;

    private bool canAttack = true;
    private bool isAttacking = false;

    private Rigidbody2D rb;

    private BossCombat
    bossCombat;

    private Transform player;

    private Animator animator;


    private void Awake()
    {
        bossCombat =
            GetComponent<
                BossCombat
            >();

        animator =
            GetComponent<
                Animator
            >();
        rb =
            GetComponent<
            Rigidbody2D
            >();
    }

    private void Start()
    {
        player =
        GameObject
        .FindGameObjectWithTag(
            "Player"
        )
        .transform;
    }

    private void Update()
    {
        if (
            player == null
        )
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

        BossData data =
            bossCombat
            .GetBossData();

        if (!canAttack)
            return;

        if (
            distance <=
            data.meleeRange
        )
        {
            StartCoroutine(
                PunchAttack()
            );
        }
        else if (
            distance <=
            data.spikesRange
        )
        {
            StartCoroutine(
                SpikeAttack()
            );
        }
        else
        {
            StartCoroutine(
                LaserAttack()
            );
        }
    }

    private IEnumerator PunchAttack()
    {
        canAttack = false;
        isAttacking = true;

        rb.linearVelocity =
            Vector2.zero;

        Instantiate(
        attackCuePrefab,
        transform.position,
        Quaternion.identity
        );

        yield return
        new WaitForSeconds(
            attackWindup
        );

        bossPunchAttack.Attack();

        yield return
        new WaitForSeconds(
            0.5f
        );

        if (
            Random.value <
            (backstepChance + 0.35f)
        )
        {
            yield return
            StartCoroutine(
                Backstep()
            );
        }

        isAttacking = false;

        yield return
        new WaitForSeconds(
            bossCombat
            .GetBossData()
            .attackCooldown
        );


        canAttack = true;
    }

    private IEnumerator SpikeAttack()
    {
        canAttack = false;
        isAttacking = true;

        rb.linearVelocity =
            Vector2.zero;

        Instantiate(
        attackCuePrefab,
        transform.position,
        Quaternion.identity
        );

        yield return
        new WaitForSeconds(
            attackWindup
        );

        bossSpikeAttack.Attack();

        yield return
        new WaitForSeconds(
            0.5f
        );

        if (
            Random.value <
            backstepChance
        )
        {
            yield return
            StartCoroutine(
                Backstep()
            );
        }
        isAttacking = false;

        yield return
        new WaitForSeconds(
            bossCombat
            .GetBossData()
            .attackCooldown
        );


        canAttack = true;
    }

    private IEnumerator LaserAttack()
    {
        canAttack = false;
        isAttacking = true;

        rb.linearVelocity = Vector2.zero;

        // 1. Memunculkan tanda aba-aba (Visualcue_0) di posisi Boss
        if (attackCuePrefab != null)
        {
            Instantiate(attackCuePrefab, transform.position, Quaternion.identity);
        }

        // 2. [TAMBAHKAN INI] Picu animasi menembak tepat saat aba-aba muncul!
        if (animator != null)
        {
            animator.SetTrigger("Laser"); // Pastikan di Animator Controller sudah ada parameter Trigger bernama "Laser"
        }

        // Boss diam sejenak bersiap menembak selama durasi Attack Windup (di Inspector kamu set 1 detik)
        yield return new WaitForSeconds(attackWindup);

        // 3. Peluru laser lahir dan meluncur ke arah Player
        bossLaserAttack.Attack();
        isAttacking = false;

        // Menunggu cooldown (di Data kamu set 2 detik) sebelum bisa menyerang lagi
        yield return new WaitForSeconds(
            bossCombat.GetBossData().attackCooldown
        );

        canAttack = true;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void StopMovement()
    {
        rb.linearVelocity =
            Vector2.zero;
    }

    private IEnumerator Backstep()
    {
        Vector2 direction =
        (
            transform.position
            -
            player.position
        ).normalized;

        float timer = 0f;

        while (
            timer < backstepDuration
        )
        {
            rb.linearVelocity =
                direction *
                backstepSpeed;

            timer +=
                Time.deltaTime;

            yield return null;
        }

        rb.linearVelocity =
            Vector2.zero;
    }
}