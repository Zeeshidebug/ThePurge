using TMPro;
using UnityEngine;

public class HUDManager
: MonoBehaviour
{
    public static
    HUDManager
    Instance;

    [SerializeField]
    private TMP_Text
    soulFragmentText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateSoulFragments(
        int amount
    )
    {
        soulFragmentText.text =
            "SOULS: "
            + amount;
    }
}