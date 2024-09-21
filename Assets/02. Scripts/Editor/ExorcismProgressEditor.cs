using UnityEngine;
using UnityEditor;

public class ExorcismProgressEditor : EditorWindow
{
    [MenuItem("TT/Exorcism Progress Manager")]
    public static void ShowWindow()
    {
        GetWindow<ExorcismProgressEditor>("Exorcism Progress Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Exorcism Progress Management", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear Exorcism Progress"))
        {
            SaveExorcismProgress.ClearProgress();
            Debug.Log("Exorcism progress cleared.");
        }

        bool hasReachedExorcismScene = SaveExorcismProgress.HasReachedExorcismScene();
        GUILayout.Label("Has reached exorcism scene: " + hasReachedExorcismScene);
    }
}