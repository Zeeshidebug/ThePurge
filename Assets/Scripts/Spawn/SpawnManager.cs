using UnityEngine;
using System.Collections.Generic;

public class SpawnManager
: MonoBehaviour
{
    [SerializeField]
    private List<SpawnData>
    enemyPool;

    [SerializeField]
    private GameObject
    bossPrefab;

    public int SpawnEnemies(
    List<SpawnPoint>
    spawnPoints
)
    {
        int enemyAmount =
            Random.Range(
                2,
                spawnPoints.Count + 1
            );

        List<SpawnPoint>
        availablePoints =
        new List<SpawnPoint>(
            spawnPoints
        );

        for (
            int i = 0;
            i < enemyAmount;
            i++
        )
        {
            int randomPoint =
                Random.Range(
                    0,
                    availablePoints.Count
                );

            SpawnPoint point =
                availablePoints[
                    randomPoint
                ];

            availablePoints.RemoveAt(
                randomPoint
            );

            SpawnData data =
                enemyPool[
                    Random.Range(
                        0,
                        enemyPool.Count
                    )
                ];

            Instantiate(
                data.prefab,
                point.transform.position,
                Quaternion.identity
            );
        }
        return enemyAmount;
    }

    public void SpawnBoss(
    SpawnPoint bossPoint
)
    {
        Instantiate(
            bossPrefab,
            bossPoint.transform.position,
            Quaternion.identity
        );
    }
}