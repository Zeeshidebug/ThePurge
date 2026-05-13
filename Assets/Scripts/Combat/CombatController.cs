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
        HandleSlot1Attack();
        HandleSlot2Attack();

        slot1CooldownTimer -= Time.deltaTime;
        slot2CooldownTimer -= Time.deltaTime;
    }

    private void HandleSlot1Attack()
    {
        WeaponData weapon =
            inventory.slot1.equippedWeapon;

        if (weapon == null) return;

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (input && slot1CooldownTimer <= 0f)
        {
            ExecuteAttack(weapon);

            slot1CooldownTimer = weapon.attackSpeed;
        }
    }

    private void HandleSlot2Attack()
    {
        WeaponData weapon =
            inventory.slot2.equippedWeapon;

        if (weapon == null) return;

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(1)
            : Input.GetMouseButtonDown(1);

        if (input && slot2CooldownTimer <= 0f)
        {
            ExecuteAttack(weapon);

            slot2CooldownTimer = weapon.attackSpeed;
        }
    }

    private void ExecuteAttack(WeaponData weapon)
    {
        switch (weapon.weaponType)
        {
            case WeaponType.Melee:
                slashAttack.PerformAttack();
                break;

            case WeaponType.Ranged:
                rangeAttack.PerformAttack();
                break;
        }
    }
}