using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private WeaponData weaponData;

    private Rigidbody2D rb;

    private bool playerNearby;
    private bool canPickup = false;
    private SpriteRenderer spriteRenderer;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke(nameof(EnablePickup), 0.2f);
    }

    private void EnablePickup()
    {
        canPickup = true;
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameplay())
            return;

        if (
            playerNearby &&
            canPickup &&
            Input.GetKeyDown(KeyCode.E)
        )
        {
            bool pickedUp =
                playerInventory
                .PickupWeapon(
                    weaponData, this
                );

            Debug.Log(
"Picked Up: " + pickedUp
);

            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerNearby = true;

        playerInventory =
            collision.GetComponent<PlayerInventory>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerNearby = false;

        playerInventory = null;
    }

    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;

        spriteRenderer.sprite = weaponData.weaponSprite;
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}