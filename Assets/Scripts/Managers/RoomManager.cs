using UnityEngine;
using System.Collections.Generic;

public class RoomManager
: MonoBehaviour
{
    public static
    RoomManager Instance;

    [SerializeField]
    private List<GameObject>
    roomPool;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private SpawnManager spawnManager;

    private GameObject
    currentRoom;
    private int lastRoomIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateRoom();
    }

    public void GenerateRoom()
    {
        EnemyCombat[] enemies =
        FindObjectsByType<
            EnemyCombat
        >(
            FindObjectsSortMode.None
        );

        foreach (
            EnemyCombat enemy
            in enemies
        )
        {
            Destroy(
                enemy.gameObject
            );
        }


        if (
            currentRoom != null
        )
        {
            Destroy(
                currentRoom
            );
        }

        int randomIndex;

        do
        {
            randomIndex =
            Random.Range(
                0,
                roomPool.Count
            );

        }
        while (
            randomIndex
            ==
            lastRoomIndex
        );

        lastRoomIndex =
            randomIndex;

        currentRoom =
        Instantiate(
            roomPool[
                randomIndex
            ],
            Vector3.zero,
            Quaternion.identity
        );

        Transform spawnPoint =
        currentRoom
        .transform
        .Find(
            "SpawnPoints/PlayerSpawn"
        );

        if (
            spawnPoint != null
        )
        {
            player.position =
                spawnPoint.position;
        }

        SpawnPoint[] points =
        currentRoom
        .GetComponentsInChildren<
            SpawnPoint
        >();

        List<SpawnPoint>
        enemyPoints =
        new List<SpawnPoint>();

        foreach (
            SpawnPoint point
            in points
        )
        {
            if (
                point.category
                ==
                SpawnCategory.Enemy
            )
            {
                enemyPoints.Add(
                    point
                );
            }
        }

        spawnManager
        .SpawnEnemies(
            enemyPoints
        );
    }
}