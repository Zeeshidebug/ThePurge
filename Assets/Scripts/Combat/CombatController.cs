using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory inventory;

    [SerializeField] private RangedAttack rangeAttack;
    [SerializeField] private SlashAttack slashAttack;

    private float slot1CooldownTimer;
    private float slot2CooldownTimer;

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameplay())
            return;

        HandleSlot1Attack();
        HandleSlot2Attack();

        slot1CooldownTimer -= Time.deltaTime;
        slot2CooldownTimer -= Time.deltaTime;
    }

    private float GetAttackCooldown(WeaponData weapon)
    {
        return weapon.attackSpeed;
    }

    private void HandleSlot1Attack()
    {
        if (inventory.slot1.isLocked)
            return;

        WeaponData weapon =
            inventory.slot1.equippedWeapon;

        if (weapon == null)
            return;

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (input && slot1CooldownTimer <= 0f)
        {
            ExecuteAttack(weapon);

            slot1CooldownTimer =
                GetAttackCooldown(weapon);
        }
    }

    private void HandleSlot2Attack()
    {
        if (inventory.slot2.isLocked)
            return;

        WeaponData weapon =
            inventory.slot2.equippedWeapon;

        if (weapon == null)
            return;

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(1)
            : Input.GetMouseButtonDown(1);

        if (input && slot2CooldownTimer <= 0f)
        {
            ExecuteAttack(weapon);

            slot2CooldownTimer =
                GetAttackCooldown(weapon);
        }
    }

    private void ExecuteAttack(WeaponData weapon)
    {
        switch (weapon.weaponType)
        {
            case WeaponType.Melee:
                slashAttack.PerformAttack(weapon);
                break;

            case WeaponType.Ranged:
                rangeAttack.PerformAttack();
                break;
        }
    }
}