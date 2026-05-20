using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private GameObject lockIcon;

    public void UpdateSlot(InventorySlot slot)
    {
        // Empty slot
        if (slot.equippedWeapon == null)
        {
            weaponIcon.enabled = false;
        }
        else
        {
            weaponIcon.enabled = true;

            weaponIcon.sprite =
                slot.equippedWeapon.weaponSprite;
        }

        // Lock state
        lockIcon.SetActive(slot.isLocked);
    }
}