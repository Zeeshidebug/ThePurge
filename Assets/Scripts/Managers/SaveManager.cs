using UnityEngine;

public class SaveManager
: MonoBehaviour
{
    public static
    SaveManager
    Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt(
            "SoulFragments",
            PlayerStats
            .Instance
            .GetSoulFragments()
        );

        PlayerPrefs.Save();

        Debug.Log(
            "GAME SAVED 💾, " + "SAVING SOULS: "
    + PlayerStats
    .Instance
    .GetSoulFragments()
        );
    }

    public int LoadSoulFragments()
    {
        int souls =
            PlayerPrefs.GetInt(
                "SoulFragments",
                0
            );

        Debug.Log(
            "LOADING SOULS: "
            + souls
        );

        return souls;
    }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey(
            "SoulFragments"
        );
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(
            "SoulFragments"
        );
    }
}