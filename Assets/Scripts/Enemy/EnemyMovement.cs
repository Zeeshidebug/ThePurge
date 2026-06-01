using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;

    private EnemyCombat enemyCombat;

    private AIPath aiPath;

    private void Awake()
    {
        enemyCombat =
            GetComponent<
                EnemyCombat
            >();

        aiPath =
            GetComponent<
                AIPath
            >();
    }

    private void Start()
    {
        GameObject playerObject =
        GameObject
        .FindGameObjectWithTag(
            "Player"
        );

        if (
            playerObject != null
        )
        {
            player =
                playerObject.transform;

            aiPath.destination =
                player.position;
        }

        aiPath.maxSpeed =
        enemyCombat
        .GetEnemyData()
        .moveSpeed;
    }

    private void Update()
    {
        if (
            !GameStateManager
            .Instance
            .IsGameplay()
        )
        {
            aiPath.canMove =
                false;

            return;
        }

        aiPath.canMove =
            true;

        EnemyData data =
        enemyCombat
        .GetEnemyData();

        float distance =
        Vector2.Distance(
            transform.position,
            player.position
        );

        if (
            distance
            <=
            data.attackRange
        )
        {
            aiPath.canMove =
                false;

            return;
        }

        if (
            player != null
        )
        {
            aiPath.destination =
                player.position;

            aiPath.SearchPath();
        }
    }
}