using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    private AIPath aiPath;
    private Animator anim; 

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        anim = GetComponent<Animator>(); 
    }

    public void UpdateAnimation()
    {
        if (aiPath == null) return;

        if (aiPath.desiredVelocity.magnitude > 0.1f && aiPath.canMove)
        {
            if (aiPath.desiredVelocity.x > 0.01f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (aiPath.desiredVelocity.x < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    // --- SEKARANG FUNGSI INI FOKUS UNTUK MEMUTAR VISUAL ANIMASI ---
    public void PlayAttackAnimation() 
    {
        if (anim != null)
        {
            // Menyalakan state animasi memukul/menembak di Animator Controller
            anim.SetTrigger("Attack"); 
        }

        // Catatan: Pemanggilan fungsi .Attack() murni untuk Melee/Ranged sudah 
        // ditangani secara otomatis dan lebih aman dari dalam EnemyMovement.cs!
    }

    public void PlayIdleAnimation() 
    {
        // Jika nanti kamu punya parameter bool "IsMoving" atau sejenisnya untuk idle, 
        // kamu bisa mengaturnya di sini agar animasi jalurnya kembali tenang.
    }
}