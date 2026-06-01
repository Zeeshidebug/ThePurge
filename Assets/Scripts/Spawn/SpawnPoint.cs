using UnityEngine;

public enum SpawnCategory
{
    Player,
    Enemy,
    Boss,
    Obstacle
}

public class SpawnPoint : MonoBehaviour
{
    public SpawnCategory category;
}