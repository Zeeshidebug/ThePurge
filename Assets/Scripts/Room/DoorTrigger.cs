using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool playerNearby;

    private void Update()
    {
        if (
            playerNearby
            &&
            Input.GetKeyDown(
                KeyCode.E
            )
        )
        {
            RoomManager
            .Instance
            .GenerateRoom();
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
}