using UnityEngine;

public static class SaveExorcismProgress
{
    public static void SaveProgress()
    {
        PlayerPrefs.SetInt("HasReachedExorcismScene", 1);
        PlayerPrefs.Save();
    }

    public static void ClearProgress()
    {
        PlayerPrefs.DeleteKey("HasReachedExorcismScene");
        PlayerPrefs.Save();
    }

    public static bool HasReachedExorcismScene()
    {
        return PlayerPrefs.HasKey("HasReachedExorcismScene") && PlayerPrefs.GetInt("HasReachedExorcismScene") == 1;
    }
}