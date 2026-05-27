using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory inventory;

    [SerializeField] private RangedAttack rangeAttack;
    [SerializeField] private SlashAttack slashAttack;

    private bool isCharging;

    private float chargeTimer;

    private WeaponData chargingWeapon;
    private float slot1CooldownTimer;
    private float slot2CooldownTimer;

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameplay())
            return;

        HandleSlot1Attack();
        HandleSlot2Attack();
        HandleCharge();

        slot1CooldownTimer -= Time.deltaTime;
        slot2CooldownTimer -= Time.deltaTime;
    }

    private float GetAttackCooldown(WeaponData weapon)
    {
        return weapon.attackSpeed;
    }

    private void HandleCharge()
    {
        if (!isCharging)
            return;

        chargeTimer += Time.deltaTime;
    }

    private void HandleSlot1Attack()
    {
        if (inventory.slot1.isLocked)
            return;

        WeaponData weapon =
            inventory.slot1.equippedWeapon;

        if (weapon == null)
            return;

        if (weapon.weaponCategory == WeaponCategory.Heavy)
        {
            HandleHeavyAttack(
                weapon,
                0
            );

            return;
        }

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (input && slot1CooldownTimer <= 0f)
        {
            DamageData damageData =
                CreateDamageData(
                    weapon
                );

            ExecuteAttack(
                weapon,
                damageData
            );

            slot1CooldownTimer =
                GetAttackCooldown(
                    weapon
                );
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

        if (weapon.weaponCategory == WeaponCategory.Heavy)
        {
            HandleHeavyAttack(
                weapon,
                1
            );

            return;
        }

        bool input =
            weapon.IsAutoAttack
            ? Input.GetMouseButton(1)
            : Input.GetMouseButtonDown(1);

        if (input && slot2CooldownTimer <= 0f)
        {
            DamageData damageData =
                CreateDamageData(
                    weapon
                );

            ExecuteAttack(
                weapon,
                damageData
            );

            slot2CooldownTimer =
                GetAttackCooldown(
                    weapon
                );
        }
    }

    private void HandleHeavyAttack(
    WeaponData weapon,
    int mouseButton)
    {
        if (
            Input.GetMouseButtonDown(
                mouseButton
            )
        )
        {
            isCharging = true;

            chargeTimer = 0f;

            chargingWeapon = weapon;
        }

        if (
            Input.GetMouseButtonUp(
                mouseButton
            )
        )
        {
            ReleaseCharge();
        }
    }

    private void ReleaseCharge()
    {
        if (!isCharging)
            return;

        isCharging = false;

        if (
            chargeTimer <
            chargingWeapon.minChargeTime
        )
        {
            return;
        }

        float chargePercent =
            Mathf.Clamp01(
                chargeTimer /
                chargingWeapon.maxChargeTime
            );

        float damageMultiplier =
            Mathf.Lerp(
                chargingWeapon.minChargeMultiplier,
                chargingWeapon.maxChargeMultiplier,
                chargePercent
            );

        DamageData damageData =
            CreateDamageData(
                chargingWeapon
            );

        damageData.chargeMultiplier =
            damageMultiplier;

        damageData.isMaxCharge =
            chargeTimer >=
            chargingWeapon.maxChargeTime;

        Debug.Log(
            "Damage: " +
            (damageData.damage *
            damageData.chargeMultiplier)
        );

        ExecuteAttack(
            chargingWeapon, damageData
        );
    }

    private void ExecuteAttack(WeaponData weapon, DamageData damageData)
    {
        switch (weapon.weaponType)
        {
            case WeaponType.Melee:
                HitData hitData =
                    slashAttack.PerformAttack(
                        weapon,
                        damageData
                    );

                if (hitData != null)
                {
                    CombatFeedbackData data =
                        GetFeedbackData(
                            weapon,
                            damageData.isMaxCharge
                        );

                    ApplyFeedback(
                        data
                    );
                }
                break;

            case WeaponType.Ranged:
                rangeAttack.PerformAttack(weapon, damageData);
                break;
        }
    }

    private CombatFeedbackData
GetFeedbackData(
    WeaponData weapon,
    bool isMaxCharge
)
    {
        CombatFeedbackData data =
            new CombatFeedbackData();

        switch (weapon.weaponCategory)
        {
            case WeaponCategory.Light:

                data.hitStop = .03f;
                data.shakeDuration = .05f;
                data.shakeStrength = .05f;

                break;

            case WeaponCategory.Heavy:

                if (isMaxCharge)
                {
                    data.hitStop = .15f;
                    data.shakeDuration = .2f;
                    data.shakeStrength = .25f;
                }
                else
                {
                    data.hitStop = .08f;
                    data.shakeDuration = .08f;
                    data.shakeStrength = .08f;
                }

                break;
        }

        return data;
    }

    private void ApplyFeedback(
    CombatFeedbackData data
)
    {
        if (data.hitStop > 0)
        {
            HitStopManager.Instance
            .Stop(
                data.hitStop
            );
        }

        CameraShake.Instance
        .Shake(
            data.shakeDuration,
            data.shakeStrength
        );
    }

    private DamageData CreateDamageData(
    WeaponData weapon
)
    {
        DamageData damageData =
            new DamageData();

        damageData.damage =
            weapon.damage;

        damageData.chargeMultiplier = 1f;

        damageData.critMultiplier =
        weapon.critMultiplier;

        float effectRoll =
        Random.Range(
            0f,
            100f
        );

        if (
            effectRoll <=
            weapon.effectChance
        )
        {
            damageData.statusEffect =
                weapon.statusEffect;

            damageData.effectDuration =
                weapon.effectDuration;

            damageData.effectDamage =
                weapon.effectDamage;
        }

        float randomRoll =
        Random.Range(
            0f,
            100f
        );

        damageData.isCritical =
            randomRoll <=
            weapon.critChance;

        damageData.isMaxCharge = false;

        damageData.source =
            gameObject;

        return damageData;
    }
}