using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool playerNearby;
    private bool isLocked;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); 
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

private void Start()
    {
        if (animator != null)
        {
           animator.SetBool("isLocked", isLocked);
        }
    }

    private void Update()
    {
        if (
            playerNearby
            &&
            !isLocked
            &&
            Input.GetKeyDown(
                KeyCode.E
            )
        )
        {
            RoomManager
            .Instance
            .NextRoom();
        }
    }

    private void OnTriggerEnter2D(
        Collider2D collision
    )
    {
        if (
            collision.CompareTag(
                "Player"
            )
        )
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(
        Collider2D collision
    )
    {
        if (
            collision.CompareTag(
                "Player"
            )
        )
        {
            playerNearby = false;
        }
    }

    public void LockDoor()
    {
        isLocked = true;

        if (animator != null)
        {
            animator.SetBool("isLocked", true);
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;

        if (animator != null)
        {
            animator.SetBool("isLocked", false);
        }

    }
}