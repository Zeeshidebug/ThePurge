using TMPro;
using UnityEngine;

public class HitText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fadeSpeed = 2f;

    private TextMeshPro textMesh;

    private Vector3 moveDirection;

    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        // Random arah gerak
        float randomX = Random.Range(-0.5f, 0.5f);

        moveDirection = new Vector3(randomX, 1f, 0f).normalized;

        textColor = textMesh.color;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Gerak
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Fade out
        textColor.a -= fadeSpeed * Time.deltaTime;

        textMesh.color = textColor;
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}