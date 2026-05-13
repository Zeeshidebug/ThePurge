using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private GameObject weaponPickupPrefab;

    [Header("Inventory Slots")]
    public InventorySlot slot1;
    public InventorySlot slot2;

    private void Start()
    {
        DebugInventory();
    }

    public void EquipWeapon(WeaponData weapon, int slotIndex)
    {
        InventorySlot targetSlot =
            slotIndex == 1 ? slot1 : slot2;

        targetSlot.equippedWeapon = weapon;

        HandleDualWield(weapon, slotIndex);

        DebugInventory();
    }

    private void HandleDualWield(WeaponData weapon, int slotIndex)
    {
        // Reset lock dulu
        slot1.isLocked = false;
        slot2.isLocked = false;

        if (weapon.OccupyTwoSlots)
        {
            if (slotIndex == 1)
            {
                slot2.isLocked = true;
            }
            else
            {
                slot1.isLocked = true;
            }
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

    public void PickupWeapon(WeaponData weapon)
    {
        // Slot 1 kosong
        if (slot1.equippedWeapon == null)
        {
            EquipWeapon(weapon, 1);
            return;
        }

        // Slot 2 kosong
        if (slot2.equippedWeapon == null)
        {
            EquipWeapon(weapon, 2);
            return;
        }

        // SAVE OLD WEAPON DULU
        WeaponData oldWeapon =
            slot1.equippedWeapon;

        // DROP OLD WEAPON
        DropWeapon(oldWeapon);

        // REPLACE SLOT
        EquipWeapon(weapon, 1);

        Debug.Log("Inventory Full - Replaced Slot 1");
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
}