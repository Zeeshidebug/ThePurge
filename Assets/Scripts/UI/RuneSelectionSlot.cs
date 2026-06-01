using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuneSelectionSlot
: MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TMP_Text runeName;

    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private Button
    selectButton;


    private RuneData runeData;

    private void Start()
    {
        selectButton.onClick
        .AddListener(
            SelectRune
        );
    }

    public void Setup(
        RuneData rune
    )
    {
        runeData =
            rune;

        icon.sprite =
            rune.icon;

        runeName.text =
            rune.runeName;

        description.text =
            rune.description;
    }

    public RuneData GetRune()
    {
        return runeData;
    }

    private void SelectRune()
    {
        RuneSelectionManager
        .Instance
        .SelectRune(
            runeData
        );
    }


}