using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySwapUI : MonoBehaviour
{
    public static InventorySwapUI Instance;

    private PlayerInventory currentInventory;

    [SerializeField]
    private Image slot1Icon;

    [SerializeField]
    private TMP_Text slot1Name;

    [SerializeField]
    private Image slot2Icon;

    [SerializeField]
    private TMP_Text slot2Name;

    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }

    public void Open(PlayerInventory inventory)
    {
        currentInventory = inventory;

        RefreshUI();

        gameObject.SetActive(true);

        GameStateManager.Instance.SetState(
            GameState.UI
        );
    }

    public void Close()
    {
        gameObject.SetActive(false);

        GameStateManager.Instance.SetState(
            GameState.Gameplay
        );
    }

    public void ChooseSlot(int slotIndex)
    {
        currentInventory.ReplaceWeapon(slotIndex);

        Close();
    }

    private void RefreshUI()
    {
        UpdateSlot(
            currentInventory.slot1,
            slot1Icon,
            slot1Name
        );

        UpdateSlot(
            currentInventory.slot2,
            slot2Icon,
            slot2Name
        );
    }

    private void UpdateSlot(
    InventorySlot slot,
    Image icon,
    TMP_Text nameText
)
    {
        if (
            slot.equippedWeapon == null
        )
        {
            icon.enabled = false;

            nameText.text =
                "Empty";

            return;
        }

        icon.enabled = true;

        icon.sprite =
            slot.equippedWeapon.weaponSprite;

        nameText.text =
            slot.equippedWeapon.weaponName;
    }
}