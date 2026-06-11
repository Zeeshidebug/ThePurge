using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [Header("UI Components")]
    public Image healthBarImage;
    public Image hudFrameImage;

    [Header("HUD Sprites (Order: Full to Empty)")]
    public Sprite[] hudSprites;

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
   
        float healthPercentage = currentHealth / maxHealth;

        healthBarImage.fillAmount = healthPercentage;

        if (healthPercentage > 0.75f)
        {
            hudFrameImage.sprite = hudSprites[0];
        }
        else if (healthPercentage > 0.50f)
        {
            hudFrameImage.sprite = hudSprites[1];
        }
        else if (healthPercentage > 0.25f)
        {
            hudFrameImage.sprite = hudSprites[2];
        }
        else
        {
            hudFrameImage.sprite = hudSprites[3];
        }
    }
}