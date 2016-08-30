///Created on 26/8/2016
///B REVANTH PRASAD

///-----------------------------------------------------------------------------------------------
///Tool to create module with required folder sturcture
///-----------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Empowerlabs.Editor
{
	public class ModuleCreatorWindow : EditorWindow
	{
	    #region Variables
	    private static ModuleCreatorWindow m_curWindow;
	    private ModulesGUIDsHolder m_guidsHolder;
	    private string m_moduleName , m_customPath;                                           //Name of the module
	    private bool m_createResource, m_createScripts, m_createEditor, m_createExampleScene; //Booleans to create folders
		private GUISkin m_skin;
	    #endregion

	    #region Menu Items
	    [MenuItem("Empowerlabs/Tools/Module Creator")]
	    private static void InitModuleTool()
	    {
	        m_curWindow = EditorWindow.GetWindow<ModuleCreatorWindow>();
			m_curWindow.minSize = new Vector2(400, 350);
			m_curWindow.maxSize = new Vector2(400, 350);
	        GUIContent content = new GUIContent();
	        content.text = "Module Tool";
	        m_curWindow.titleContent = content;
	    }
	    #endregion

	    #region Methods 

		private void OnEnable()
		{
			m_skin = Resources.Load ("Editor Skins/CustomSkin", typeof(GUISkin)) as GUISkin;
		}

	    private void OnGUI()
	    {
			EditorGUILayout.BeginVertical(m_skin.GetStyle("window"));

			EditorGUILayout.LabelField ("Module Creator", m_skin.GetStyle ("header"));
			EditorGUILayout.LabelField ("Tool to create module with required folder sturcture", m_skin.GetStyle ("tooltip"));
			GUILayout.Space (10);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Module Name" , m_skin.GetStyle("label"));
			m_moduleName = EditorGUILayout.TextField ( m_moduleName , m_skin.GetStyle("textfield"));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (20);

			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Select all" , m_skin.GetStyle("button"))) {
				m_createEditor = m_createExampleScene = m_createResource = m_createScripts = true;
			}
	                GUILayout.Space(5);
			if (GUILayout.Button ("Select none", m_skin.GetStyle("button"))) {
				m_createEditor = m_createExampleScene = m_createResource = m_createScripts = false;
			}
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (10);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Create Editor" , m_skin.GetStyle("label"));
			m_createEditor = EditorGUILayout.Toggle ( m_createEditor ,m_skin.GetStyle("toggle"));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (5);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Create Resource" , m_skin.GetStyle("label"));
			m_createResource = EditorGUILayout.Toggle (m_createResource, m_skin.GetStyle ("toggle"));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (5);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Create ExampleScene" , m_skin.GetStyle("label"));
			m_createExampleScene = EditorGUILayout.Toggle (m_createExampleScene, m_skin.GetStyle ("toggle"));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (5);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Create Scripts" , m_skin.GetStyle("label"));
			m_createScripts = EditorGUILayout.Toggle (m_createScripts, m_skin.GetStyle ("toggle"));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (10);
	        
			if (!System.String.IsNullOrEmpty (m_moduleName)) {
				if (GUILayout.Button ("Create Module" , m_skin.GetStyle("button"))) {
					CreateModule ();
				}
			}
	        EditorGUILayout.EndVertical();

			Repaint ();
	    }

	    /// <summary>
	    /// Checks whether the module is already exits or not
	    /// </summary>
	    /// <returns>Module Exitences</returns>
	    private bool CheckForModuleExistence(string name)
	    {
	        return false;
	    }

	    //Creates Modules based on the selected hierarchies
	    private void CreateModule()
	    {
	        string guid = AssetDatabase.CreateFolder("Assets", m_moduleName);

	        //Editor Folder Structure
	        if (m_createEditor)
	        {
	            string editorGuid = AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(guid), "Editor");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(editorGuid), "Window");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(editorGuid), "Inspector");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(editorGuid), "Utils");
	        }

	        if (m_createExampleScene)
	        {
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(guid), "Scenes");
	        }

	        if (m_createResource)
	        {
	            string resGuid = AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(guid), "Resources");

	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Textures");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Sprites");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Prefabs");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Materials");
				AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Fonts");

				if (m_createEditor) 
				{
					AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(resGuid), "Editor Skins");
				}
	        }

	        if (m_createScripts)
	        {
	            string scriptsGuid = AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(guid), "Scripts");

	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(scriptsGuid), "Controllers");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(scriptsGuid), "Managers");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(scriptsGuid), "Utils");
	            AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(scriptsGuid), "Others");
	        }
	    }
	    #endregion

	}//end of class
}