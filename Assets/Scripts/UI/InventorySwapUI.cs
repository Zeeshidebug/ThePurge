using UnityEngine;

public class InventorySwapUI : MonoBehaviour
{
    public static InventorySwapUI Instance;

    private PlayerInventory currentInventory;

    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }

    public void Open(PlayerInventory inventory)
    {
        currentInventory = inventory;

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
}