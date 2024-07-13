using UnityEditor;
using UnityEngine;

namespace TT
{
    public class ClueDatabaseEditor : EditorWindow
    {
        private ClueDatabase clueDatabase;
        private Vector2 scrollPos;

        [MenuItem("TT/Clue Database Editor")]
        public static void ShowWindow()
        {
            GetWindow<ClueDatabaseEditor>("Clue Database Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Clue Database Editor", EditorStyles.boldLabel);

            clueDatabase = (ClueDatabase)EditorGUILayout.ObjectField("Clue Database", clueDatabase, typeof(ClueDatabase), false);

            if (clueDatabase == null)
                return;

            if (GUILayout.Button("Add New Clue"))
            {
                Clue newClue = CreateInstance<Clue>();
                newClue.name = "New Clue";
                AssetDatabase.AddObjectToAsset(newClue, clueDatabase);
                clueDatabase.Clues.Add(newClue);
                EditorUtility.SetDirty(clueDatabase);
                AssetDatabase.SaveAssets();
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            foreach (Clue clue in clueDatabase.Clues)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(clue.name, GUILayout.Width(200));
                if (GUILayout.Button("Edit", GUILayout.Width(50)))
                {
                    Selection.activeObject = clue;
                }
                if (GUILayout.Button("Delete", GUILayout.Width(50)))
                {
                    clueDatabase.Clues.Remove(clue);
                    DestroyImmediate(clue, true);
                    EditorUtility.SetDirty(clueDatabase);
                    AssetDatabase.SaveAssets();
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
    }
}