using System.Collections.Generic;
using UnityEngine;

public class LootChest
: MonoBehaviour
{
    [SerializeField]
    private LootDatabase
    mainLootDatabase;

    [SerializeField]
    private LootDatabase
    bonusLootDatabase;

    [SerializeField]
    private WeaponDatabase
    weaponDatabase;

    [SerializeField]
    private EquipmentDatabase
    equipmentDatabase;

    [SerializeField]
    [Range(0f, 1f)]
    private float bonusLootChance =
    0.5f;

    [SerializeField]
    private GameObject
    lootPickupPrefab;

    [SerializeField]
    private bool opened;

    private bool locked = true;

    private void OnTriggerStay2D(
        Collider2D collision
    )
    {
        if (opened)
            return;

        if (
            !collision.CompareTag(
                "Player"
            )
        )
            return;

        if (
            Input.GetKeyDown(
                KeyCode.E
            )
        )
        {
            if (locked)
            {
                Debug.Log(
                    "CHEST LOCKED 🔒"
                );

                return;
            }

            OpenChest();
            Debug.Log(
                "CHEST OPENED 🗝️"
            );
        }
    }

    private void OpenChest()
    {
        LootData mainReward =
        RollLoot(
            mainLootDatabase
        );

        switch (
            mainReward.lootType
        )
        {
            case LootType.WeaponReward:

                WeaponData weapon =
                    RollWeapon();

                SpawnWeapon(
                    weapon
                );

                break;

            case LootType.EquipmentReward:

                EquipmentData equipment =
                    RollEquipment();

                SpawnEquipment(
                    equipment
                );

                break;

            case LootType.RuneReward:

                SpawnLoot(
                    mainReward
                );

                break;

            case LootType.SoulFragment:

                SpawnLoot(
                    mainReward
                );

                break;
        }
        if (
            Random.value
            <=
            bonusLootChance
        )
        {
            LootData bonusReward =
            RollLoot(
                bonusLootDatabase
            );

            SpawnLoot(
                bonusReward
            );
        }
    }

    private LootData RollLoot(LootDatabase database)
    {
        int totalWeight = 0;

        foreach (
            LootData loot
            in database.lootPool
        )
        {
            totalWeight +=
                loot.weight;
        }

        int roll =
            Random.Range(
                0,
                totalWeight
            );

        int currentWeight = 0;

        foreach (
            LootData loot
            in database.lootPool
        )
        {
            currentWeight +=
                loot.weight;

            if (
                roll <
                currentWeight
            )
            {
                return loot;
            }
        }

        return null;
    }

    private WeaponData RollWeapon()
    {
        int randomIndex =
        Random.Range(
            0,
            weaponDatabase.weaponPool.Count
        );

        return
        weaponDatabase.weaponPool[
            randomIndex
        ];
    }

    private EquipmentData RollEquipment()
    {
        int randomIndex =
        Random.Range(
            0,
            equipmentDatabase
            .equipmentPool
            .Count
        );

        return
        equipmentDatabase
        .equipmentPool[
            randomIndex
        ];
    }

    private void SpawnLoot(
    LootData loot
)
    {
        Vector2 spawnOffset =
        Random.insideUnitCircle
        * 0.25f;

        GameObject pickup =
        Instantiate(
            lootPickupPrefab,

            transform.position
            +
            (Vector3)spawnOffset,

            Quaternion.identity
        );

        LootPickup
        lootPickup =
        pickup.GetComponent<
            LootPickup
        >();

        lootPickup.Initialize(
            loot
        );

        Rigidbody2D rb =
        pickup.GetComponent<
            Rigidbody2D
        >();

        if (
            rb != null
        )
        {
            Vector2 launchDirection =
                Random.insideUnitCircle
                .normalized;

            rb.AddForce(
                launchDirection * 4f,
                ForceMode2D.Impulse
            );
        }


    }

    private void SpawnWeapon(
    WeaponData weapon
)
    {
        Vector2 spawnOffset =
            Random.insideUnitCircle
            * 0.25f;

        GameObject pickup =
        Instantiate(
            lootPickupPrefab,

            transform.position
            +
            (Vector3)spawnOffset,

            Quaternion.identity
        );

        LootPickup lootPickup =
            pickup.GetComponent<
                LootPickup
            >();

        lootPickup.Initialize(
            weapon
        );

        Rigidbody2D rb =
            pickup.GetComponent<
                Rigidbody2D
            >();

        if (rb != null)
        {
            Vector2 launchDirection =
                Random.insideUnitCircle
                .normalized;

            rb.AddForce(
                launchDirection * 4f,
                ForceMode2D.Impulse
            );
        }
    }

    private void SpawnEquipment(
    EquipmentData equipment
)
    {
        Vector2 spawnOffset =
            Random.insideUnitCircle
            * 0.25f;

        GameObject pickup =
        Instantiate(
            lootPickupPrefab,

            transform.position
            +
            (Vector3)spawnOffset,

            Quaternion.identity
        );

        LootPickup lootPickup =
            pickup.GetComponent<
                LootPickup
            >();

        lootPickup.Initialize(
            equipment
        );

        Rigidbody2D rb =
            pickup.GetComponent<
                Rigidbody2D
            >();

        if (rb != null)
        {
            Vector2 launchDirection =
                Random.insideUnitCircle
                .normalized;

            rb.AddForce(
                launchDirection * 4f,
                ForceMode2D.Impulse
            );
        }
    }

    public void LockChest()
    {
        locked = true;
    }

    public void UnlockChest()
    {
        locked = false;
    }
}