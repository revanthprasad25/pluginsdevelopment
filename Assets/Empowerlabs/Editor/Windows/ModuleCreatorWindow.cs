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
	    #endregion

	    #region Menu Items
	    [MenuItem("Empowerlabs/Tools/Module Creator")]
	    private static void InitModuleTool()
	    {
	        m_curWindow = EditorWindow.GetWindow<ModuleCreatorWindow>();
	        m_curWindow.minSize = new Vector2(400, 200);
	        m_curWindow.maxSize = new Vector2(410, 210);
	        GUIContent content = new GUIContent();
	        content.text = "Module Tool";
	        m_curWindow.titleContent = content;
	    }
	    #endregion

	    #region Methods 
	    private void OnGUI()
	    {
	        GUILayout.Space(10);
	        EditorGUILayout.BeginVertical();
	            GUILayout.Space(10);
	            m_moduleName = EditorGUILayout.TextField("Module Name", m_moduleName);
	            GUILayout.Space(10);
	            EditorGUILayout.BeginHorizontal();
	                if(GUILayout.Button("Select all"))
	                {
	                    m_createEditor = m_createExampleScene = m_createResource = m_createScripts = true;
	                }
	                GUILayout.Space(5);
	                if (GUILayout.Button("Select none"))
	                {
	                    m_createEditor = m_createExampleScene = m_createResource = m_createScripts = false;
	                }
	            EditorGUILayout.EndHorizontal();
	            GUILayout.Space(10);
	            EditorGUILayout.BeginHorizontal();
	                m_createEditor = EditorGUILayout.Toggle("Create Editor", m_createEditor);
	                GUILayout.Space(5);
	                m_createResource = EditorGUILayout.Toggle("Create Resource", m_createResource);
	            EditorGUILayout.EndHorizontal();
	            GUILayout.Space(10);
	            EditorGUILayout.BeginHorizontal();
	                m_createScripts = EditorGUILayout.Toggle("Create Scripts", m_createScripts);
	                GUILayout.Space(5);
	                m_createExampleScene = EditorGUILayout.Toggle("Create ExampleScene", m_createExampleScene);
	            EditorGUILayout.EndHorizontal();
	            GUILayout.Space(10);

	            if(!System.String.IsNullOrEmpty(m_moduleName))
	            {
	                if (GUILayout.Button("Create Module"))
	                {
	                    CreateModule();
	                }
	            }
	        EditorGUILayout.EndVertical();
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