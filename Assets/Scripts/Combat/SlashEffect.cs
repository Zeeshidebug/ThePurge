using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.15f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}