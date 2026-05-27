using UnityEngine;
using System.Collections.Generic;

public class SpawnManager
: MonoBehaviour
{
    [SerializeField]
    private List<SpawnData>
    enemyPool;

    public void SpawnEnemies(
        List<SpawnPoint>
        spawnPoints
    )
    {
        foreach (
            SpawnPoint point
            in spawnPoints
        )
        {
            if (
                point.category
                !=
                SpawnCategory.Enemy
            )
                continue;

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
    }
}