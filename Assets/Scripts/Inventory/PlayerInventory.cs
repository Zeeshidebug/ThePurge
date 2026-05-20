using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class PlayerInventory : MonoBehaviour
{
    private WeaponData pendingWeapon;
    public Action OnInventoryChanged;

    [Header("Pickup Settings")]
    [SerializeField] private GameObject weaponPickupPrefab;

    [Header("Inventory Slots")]
    public InventorySlot slot1;
    public InventorySlot slot2;
    private WeaponPickup pendingPickup;

    private void Start()
    {
        DebugInventory();
    }

    public void EquipWeapon(WeaponData weapon, int slotIndex)
    {
        InventorySlot targetSlot =
            slotIndex == 1 ? slot1 : slot2;

        targetSlot.equippedWeapon = weapon;

        RefreshSlotLocks();

        OnInventoryChanged?.Invoke();

        DebugInventory();
    }

    private void RefreshSlotLocks()
    {
        slot1.isLocked = false;
        slot2.isLocked = false;

        if (
            slot1.equippedWeapon != null &&
            slot1.equippedWeapon.OccupyTwoSlots
        )
        {
            slot2.isLocked = true;
        }

        if (
            slot2.equippedWeapon != null &&
            slot2.equippedWeapon.OccupyTwoSlots
        )
        {
            slot1.isLocked = true;
        }
    }

    private void DebugInventory()
    {
        Debug.Log("=== INVENTORY ===");

        Debug.Log("Slot 1: " +
            (slot1.equippedWeapon != null
            ? slot1.equippedWeapon.weaponName
            : "Empty"));

        Debug.Log("Slot 2: " +
            (slot2.equippedWeapon != null
            ? slot2.equippedWeapon.weaponName
            : "Empty"));
    }

    public bool PickupWeapon(WeaponData weapon, WeaponPickup pickup)
    {
        if (
            slot1.equippedWeapon == null &&
            !slot1.isLocked
        )
        {
            EquipWeapon(weapon, 1);
            return true;
        }

        if (
            slot2.equippedWeapon == null &&
            !slot2.isLocked
        )
        {
            EquipWeapon(weapon, 2);
            return true;
        }

        pendingWeapon = weapon;
        pendingPickup = pickup;

        InventorySwapUI.Instance.Open(this);
        return false;
    }

    private void DropWeapon(WeaponData weapon)
    {
        if (weapon == null) return;

        GameObject droppedWeapon = Instantiate(
            weaponPickupPrefab,
            transform.position + new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f),
                0f
            ),
            Quaternion.identity
        );

        WeaponPickup pickup =
            droppedWeapon.GetComponent<WeaponPickup>();

        pickup.SetWeaponData(weapon);
        Vector2 randomDirection = new Vector2(
        Random.Range(-1f, 1f),
        Random.Range(-1f, 1f)
        ).normalized;

        pickup.Launch(randomDirection, 3f);
    }

    public void ReplaceWeapon(int slotIndex)
    {
        InventorySlot targetSlot =
            slotIndex == 1 ? slot1 : slot2;

        WeaponData oldWeapon =
            targetSlot.equippedWeapon;

        DropWeapon(oldWeapon);

        EquipWeapon(pendingWeapon, slotIndex);

        if (pendingPickup != null)
        {
            Destroy(
                pendingPickup.gameObject
            );

            pendingPickup = null;
        }

        pendingWeapon = null;
    }
}