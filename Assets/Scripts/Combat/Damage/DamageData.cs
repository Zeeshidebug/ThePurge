using UnityEngine;

[System.Serializable]
public class DamageData
{
    public float damage;

    public float chargeMultiplier;

    public bool isCritical;
    public float critMultiplier = 2f;

    public StatusEffectType statusEffect;

    public float effectDuration;

    public float effectDamage;

    public bool isMaxCharge;

    public GameObject source;

    public float FinalDamage()
    {
        float finalDamage =
            damage *
            chargeMultiplier;

        if (isCritical)
        {
            finalDamage *=
                critMultiplier;
        }

        return finalDamage;
    }
}