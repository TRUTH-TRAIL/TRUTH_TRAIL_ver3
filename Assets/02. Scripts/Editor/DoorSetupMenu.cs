using UnityEngine;
using UnityEditor;
using TMPro;

namespace TT
{
    public class DoorSetupMenu : Editor
    {
        [MenuItem("TT/Door SetUp")]
        private static void ShowWindow()
        {
            DoorSetupWindow window = (DoorSetupWindow)EditorWindow.GetWindow(typeof(DoorSetupWindow), true, "Door SetUp");
            window.Show();
        }
    }

    public class DoorSetupWindow : EditorWindow
    {
        private string openText = "Press E to Open";
        private string closeText = "Press E to Close";
        private Vector3 textPosition = Vector3.zero;
        private Color textColor = Color.white;
        private int textSize = 14;
        private TMP_FontAsset font;
        private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

        void OnGUI()
        {
            GUILayout.Label("Set Up All Doors", EditorStyles.boldLabel);

            openText = EditorGUILayout.TextField("Open Text", openText);
            closeText = EditorGUILayout.TextField("Close Text", closeText);
            textPosition = EditorGUILayout.Vector3Field("Text Position", textPosition);
            textColor = EditorGUILayout.ColorField("Text Color", textColor);
            textSize = EditorGUILayout.IntField("Text Size", textSize);
            font = (TMP_FontAsset)EditorGUILayout.ObjectField("Font", font, typeof(TMP_FontAsset), false);
            alignment = (TextAlignmentOptions)EditorGUILayout.EnumPopup("Text Alignment", alignment);

            if (GUILayout.Button("Set Up"))
            {
                SetUpAllDoors();
                this.Close();
            }
        }

        private void SetUpAllDoors()
        {
            Door[] doors = FindObjectsOfType<Door>();

            foreach (Door door in doors)
            {
                Undo.RecordObject(door, "Set Door Properties");

                if (door.TXT == null || door.RectTransform == null)
                {
                    TextMeshProUGUI textComponent = door.GetComponentInChildren<TextMeshProUGUI>();
                    if (textComponent != null)
                    {
                        door.TXT = textComponent;
                    }
                    RectTransform rectTransform = textComponent.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        door.RectTransform = rectTransform;
                    }
                }
                else
                {
                    door.OpenText = openText;
                    door.CloseText = closeText;
                    door.TextPosition = textPosition;
                    door.TextColor = textColor;
                    door.TextSize = textSize;
                    door.Font = font;
                    door.Alignment = alignment;
                }
             

                EditorUtility.SetDirty(door);
                PrefabUtility.RecordPrefabInstancePropertyModifications(door);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
