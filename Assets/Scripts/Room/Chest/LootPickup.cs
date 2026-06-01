using UnityEngine;

public class LootPickup
: MonoBehaviour
{
    private PickupType pickupType;

    private LootData lootData;

    private WeaponData weaponData;

    private EquipmentData equipmentData;
    private bool canPickup = false;

    private void Start()
    {
        Invoke(
            nameof(
                EnablePickup
            ),
            0.5f
        );
    }
    public void Initialize(
        LootData loot
    )
    {
        pickupType =
            PickupType.Loot;

        lootData =
            loot;

        SetupSprite(
            loot.icon
        );
    }

    public void Initialize(
    WeaponData weapon
)
    {
        pickupType =
            PickupType.Weapon;

        weaponData =
            weapon;

        SetupSprite(
            weapon.weaponSprite
        );
    }

    public void Initialize(
        EquipmentData equipment
    )
    {
        pickupType =
            PickupType.Equipment;

        equipmentData =
            equipment;

        SetupSprite(
            equipment.icon
        );
    }

    private void SetupSprite(
        Sprite icon
    )
    {
        SpriteRenderer sr =
            GetComponent<
                SpriteRenderer
            >();

        if (
            sr != null
            &&
            icon != null
        )
        {
            sr.sprite =
                icon;
        }
    }

    private void OnTriggerEnter2D(
        Collider2D collision
    )
    {
        if (
            !canPickup
        )
            return;
        if (
            !collision.CompareTag(
                "Player"
            )
        )
            return;

        switch (
            pickupType
        )
        {
            case PickupType.Loot:

                if (
                    lootData.lootType
                    ==
                    LootType.RuneReward
                )
                {
                    RuneSelectionManager
                    .Instance
                    .OpenRuneSelection();

                    Destroy(
                        gameObject
                    );

                    return;
                }

                if (
                    lootData.lootType
                    ==
                    LootType.SoulFragment
                )
                {
                    PlayerStats
                    .Instance
                    .AddSoulFragments(
                        lootData.soulFragmentReward
                    );

                    Debug.Log(
                        "+"
                        +
                        lootData.soulFragmentReward
                        +
                        " Soul Fragments"
                    );

                    Destroy(
                        gameObject
                    );

                    return;
                }

                break;

            case PickupType.Weapon:

                Debug.Log(
                    weaponData.weaponName
                );

                break;

            case PickupType.Equipment:

                Debug.Log(
                    equipmentData.equipmentName
                );

                break;
        }

        Destroy(
            gameObject
        );
    }

    private void EnablePickup()
    {
        canPickup = true;
    }

}