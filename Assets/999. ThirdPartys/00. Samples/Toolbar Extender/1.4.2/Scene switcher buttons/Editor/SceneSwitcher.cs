using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
	 
namespace UnityToolbarExtender.Examples
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;
	 
        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold
            };
        }
    }
	 
    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }
	      
        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if(GUILayout.Button(new GUIContent("MainMenu", "MainMenu Scene")))
            {
                SceneHelper.OpenScene("MainMenu");
            }
            
            if(GUILayout.Button(new GUIContent("Tutorial", "Tutorial Scene")))
            {
                SceneHelper.OpenScene("Tutorial");
            }
	         
            if(GUILayout.Button(new GUIContent("MainGame", "MainGame Scene")))
            {
                SceneHelper.OpenScene("MainGame");
            }
            
            if(GUILayout.Button(new GUIContent("Exorcism", "Exorcism Scene")))
            {
                SceneHelper.OpenScene("Exorcism");
            }
        }
    }
	   
    static class SceneHelper
    {
        public static void OpenScene(string name)
        {
            var saved = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            if (saved)
            {
                _ = EditorSceneManager.OpenScene($"Assets/01. Scenes/{name}.unity");
            }
        }
    }
}