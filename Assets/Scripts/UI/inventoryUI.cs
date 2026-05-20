using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory inventory;

    [Header("Slot UI")]
    [SerializeField] private InventorySlotUI slot1UI;
    [SerializeField] private InventorySlotUI slot2UI;

    private void Start()
    {
        inventory.OnInventoryChanged += RefreshUI;

        RefreshUI();
    }

    private void RefreshUI()
    {
        slot1UI.UpdateSlot(
            inventory.slot1);

        slot2UI.UpdateSlot(
            inventory.slot2);
    }
}