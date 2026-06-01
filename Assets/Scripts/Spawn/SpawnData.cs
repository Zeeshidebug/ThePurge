using UnityEngine;

[CreateAssetMenu(
    menuName = "The Purge/Spawn Data"
)]
public class SpawnData : ScriptableObject
{
    [Header("Object")]
    public GameObject prefab;

    [Header("Spawn Settings")]
    public int spawnWeight = 1;

    public int minSpawnCount = 1;

    public int maxSpawnCount = 3;
}