using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool playerNearby;
    private bool isLocked;
    private SpriteRenderer doorSprite;

    private void Awake()
    {
        doorSprite =
            GetComponent<
                SpriteRenderer
            >();
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

        doorSprite.color =
            Color.red;
    }

    public void UnlockDoor()
    {
        isLocked = false;

        doorSprite.color =
            Color.green;
    }
}