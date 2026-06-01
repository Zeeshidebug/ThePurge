using UnityEngine;

public class StatueOfGrowth
: MonoBehaviour
{
    private bool
    playerNearby;

    private void Update()
    {
        if (
            !playerNearby
        )
            return;

        if (
            Input.GetKeyDown(
                KeyCode.E
            )
        )
        {
            GrowthUIManager
            .Instance
            .OpenUI();
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
            playerNearby =
                true;
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
            playerNearby =
                false;
        }
    }
}