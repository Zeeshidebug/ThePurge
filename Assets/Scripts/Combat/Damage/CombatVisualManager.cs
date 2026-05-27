using UnityEngine;

public class CombatVisualManager : MonoBehaviour
{
    public static CombatVisualManager Instance;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject hitTextPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetHitTextPrefab()
    {
        return hitTextPrefab;
    }
}