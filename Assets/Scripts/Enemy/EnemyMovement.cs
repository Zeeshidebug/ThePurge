using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private EnemyCombat enemyCombat;
    private AIPath aiPath;
    private EnemyAnimation enemyAnim;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        aiPath = GetComponent<AIPath>();
        enemyAnim = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            aiPath.destination = player.position;
        }

        // AMAN: Kode ganda yang bikin crash di bawah sudah dihapus, tersisa yang pakai if-statement ini saja
        if (enemyCombat != null && enemyCombat.GetEnemyData() != null)
        {
            aiPath.maxSpeed = enemyCombat.GetEnemyData().moveSpeed;
        }
    }

   private void Update()
    {
        // 1. Cek State Gameplay
        if (!GameStateManager.Instance.IsGameplay())
        {
            aiPath.canMove = false;
            if (enemyAnim != null) enemyAnim.PlayIdleAnimation();
            return;
        }

        // 2. Cek apakah player ada
        if (player == null)
        {
            aiPath.canMove = false;
            if (enemyAnim != null) enemyAnim.PlayIdleAnimation();
            return;
        }

        EnemyData data = enemyCombat.GetEnemyData();
        if (data == null) return; 

        // 3. Hitung jarak
        float distance = Vector2.Distance(transform.position, player.position);

        // 4. Cek jarak serangan
        if (distance <= data.attackRange)
        {
            aiPath.canMove = false;

            // Pastikan kakinya tidak animasi berjalan saat berhenti memukul/menembak
            if (enemyAnim != null)
            {
                enemyAnim.PlayIdleAnimation();
                enemyAnim.PlayAttackAnimation(); // <-- Fungsi ini yang bertugas menyalakan animasi!
            }

            // A. Jika musuh jarak dekat (Slime, Zombie, Gandarwa)
            EnemyMeleeAttack meleeAttack = GetComponent<EnemyMeleeAttack>();
            if (meleeAttack != null)
            {
                meleeAttack.Attack();
            }

            // B. Jika musuh jarak jauh (Ghost / Penyihir)
            EnemyRangedAttack rangedAttack = GetComponent<EnemyRangedAttack>();
            if (rangedAttack != null)
            {
                rangedAttack.Attack();
            }

            return;
        }

        // 5. Jalankan pergerakan jika di luar jarak serang
        aiPath.canMove = true;
        aiPath.destination = player.position;

        // 6. Update visual animasi berjalan / membalik arah
        if (enemyAnim != null)
        {
            enemyAnim.UpdateAnimation();
        }
    }
}