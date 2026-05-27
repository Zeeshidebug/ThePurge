using UnityEngine;
public class SlashEffect : MonoBehaviour
{
    private float lifeTime;

    public void Initialize(
        WeaponData weapon
    )
    {
        lifeTime =
            weapon.slashEffectDuration;

        Destroy(
            gameObject,
            lifeTime
        );
    }
}
