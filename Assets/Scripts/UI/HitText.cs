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


    public void SetText(
        string text,
        Color color,
        float scale = 1f
    )
    {
        textMesh.text = text;

        textMesh.color = color;

        transform.localScale =
            Vector3.one * scale;
    }

    public void SetDamageText(
    DamageData damageData
)
    {
        float finalDamage =
            damageData.FinalDamage();

        string text =
            Mathf.RoundToInt(
                finalDamage
            ).ToString();

        Color color =
            Color.red;

        float scale =
            1f;

        // Crit
        if (damageData.isCritical)
        {
            text =
                "CRIT!\n" + text + "!";

            color =
                Color.yellow;

            scale =
                1.5f;
        }

        // Max charge
        else if (
            damageData.isMaxCharge
        )
        {
            color =
                new Color(
                    1f,
                    0.5f,
                    0f
                );

            scale =
                1.2f;
        }

        SetText(
            text,
            color,
            scale
        );
    }

    public static void SpawnStatusText(
    GameObject prefab,
    Vector3 position,
    string text,
    Color color
)
    {
        GameObject hitText =
            Instantiate(
                prefab,

                position
                +
                new Vector3(
                    Random.Range(
                        -0.3f,
                        0.3f
                    ),

                    Random.Range(
                        -0.3f,
                        0.3f
                    ),

                    0f
                ),

                Quaternion.identity
            );

        hitText
        .GetComponent<HitText>()
        .SetText(
            text,
            color,
            0.8f
        );
    }
}