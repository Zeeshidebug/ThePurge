using UnityEngine;

public enum SpawnCategory
{
    Player,
    Enemy,
    Obstacle
}

public class SpawnPoint : MonoBehaviour
{
    public SpawnCategory category;
}