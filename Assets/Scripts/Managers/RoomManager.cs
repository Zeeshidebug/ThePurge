using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class RoomManager
: MonoBehaviour
{

    public static
    RoomManager Instance;

    [SerializeField]
    private List<GameObject>
    combatRoomPool;

    [SerializeField]
    private GameObject
    upgradeRoom;

    [SerializeField]
    private GameObject
    bossRoom;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private SpawnManager spawnManager;

    private GameObject
    currentRoom;
    private int lastRoomIndex = -1;
    private int roomProgress = 0;
    private int enemyCount;
    private int currentWave = 1;

    [SerializeField]
    private int maxWave = 3;

    private DoorTrigger currentDoor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateRoom();
    }

    private IEnumerator
    DelayedScan()
    {
        yield return null;

        AstarPath
        .active
        .Scan();
    }

    public void GenerateRoom()
    {
        EnemyCombat[] enemies =
        FindObjectsByType<
            EnemyCombat
        >(
            FindObjectsSortMode.None
        );

        bool isUpgradeRoom =
        roomProgress == 3;

        bool isBossRoom =
        roomProgress == 5;

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
            WeaponPickup[] weapons =
            FindObjectsByType<
                WeaponPickup
            >(
                FindObjectsSortMode.None
            );

            foreach (
                WeaponPickup weapon
                in weapons
            )
            {
                Destroy(
                    weapon.gameObject
                );
            }

            LootPickup[] lootDrops =
            FindObjectsByType<
                LootPickup
            >(
                FindObjectsSortMode.None
            );

            foreach (
                LootPickup loot
                in lootDrops
            )
            {
                Destroy(
                    loot.gameObject
                );
            }

            Destroy(
                currentRoom
            );
        }

        GameObject roomToSpawn;

        if (
            roomProgress == 3
        )
        {
            roomToSpawn =
                upgradeRoom;
        }
        else if (
            roomProgress == 5
        )
        {
            roomToSpawn =
                bossRoom;
        }
        else
        {
            int randomIndex;

            do
            {
                randomIndex =
                Random.Range(
                    0,
                    combatRoomPool.Count
                );
            }
            while (
                randomIndex ==
                lastRoomIndex
            );

            lastRoomIndex =
                randomIndex;

            roomToSpawn =
                combatRoomPool[
                    randomIndex
                ];
        }

        currentRoom =
        Instantiate(
            roomToSpawn,
            Vector3.zero,
            Quaternion.identity
        );

        currentDoor =
        currentRoom
        .GetComponentInChildren<
            DoorTrigger
        >();

        StartCoroutine(DelayedScan());

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

        currentWave = 1;

        if (isBossRoom)
        {
            SpawnWave();
            LockDoor();
            currentWave = 3;
        }
        else if (!isUpgradeRoom)
        {
            SpawnWave();
            LockDoor();
            LockChest();
        }
        else
        {
            UnlockDoor();
            UnlockChest();
        }
    }

    private void SpawnWave()
    {
        SpawnPoint[] points =
        currentRoom
        .GetComponentsInChildren<
            SpawnPoint
        >();

        if (
            roomProgress == 5
        )
        {
            foreach (
                SpawnPoint point
                in points
            )
            {
                if (
                    point.category ==
                    SpawnCategory.Boss
                )
                {
                    spawnManager
                    .SpawnBoss(
                        point
                    );

                    enemyCount = 1;

                    return;
                }
            }
        }

        List<SpawnPoint>
        enemyPoints =
        new List<SpawnPoint>();

        foreach (
            SpawnPoint point
            in points
        )
        {
            if (
                point.category ==
                SpawnCategory.Enemy
            )
            {
                enemyPoints.Add(
                    point
                );
            }
        }

        enemyCount =
        spawnManager
        .SpawnEnemies(
            enemyPoints
        );
    }

    public void EnemyKilled()
    {
        enemyCount--;

        Debug.Log(
            "Enemy Left: "
            + enemyCount
        );

        Debug.Log(
    "Current Wave: "
    + currentWave
);

        if (enemyCount <= 0)
        {
            currentWave++;

            if (
                currentWave >
                maxWave
            )
            {
                UnlockDoor();
                UnlockChest();

                Debug.Log(
                    "ROOM CLEAR 😭🔥"
                );

                if (
                    roomProgress == 5
                )
                {
                    VictoryManager
                    .Instance
                    .ShowVictory();
                }
            }
            else
            {
                Debug.Log(
                    "WAVE "
                    + currentWave
                );

                SpawnWave();
            }
        }
    }

    public void NextRoom()
    {
        roomProgress++;

        Debug.Log(
            "ROOM PROGRESS: "
            + roomProgress
        );

        GenerateRoom();
    }

    private void LockDoor()
    {
        if (
            currentDoor != null
        )
        {
            currentDoor
            .LockDoor();
        }
    }

    private void LockChest()
    {
        LootChest chest =
        currentRoom
        .GetComponentInChildren<
            LootChest
        >();

        if (
            chest != null
        )
        {
            chest.LockChest();
        }
    }

    private void UnlockDoor()
    {
        if (
            currentDoor != null
        )
        {
            currentDoor
            .UnlockDoor();
        }
    }

    private void UnlockChest()
    {
        LootChest chest =
        currentRoom
        .GetComponentInChildren<
            LootChest
        >();

        if (
            chest != null
        )
        {
            chest.UnlockChest();
        }
    }
}