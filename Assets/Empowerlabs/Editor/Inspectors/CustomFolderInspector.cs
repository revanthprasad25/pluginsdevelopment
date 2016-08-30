using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;


[CustomEditor(typeof(DefaultAsset) , true)]
public class CustomFolderInspector : Editor 
{
	#region Private Variables

	GUISkin m_skin;
	string m_path;

	#endregion

	private void OnEnable()
	{
		m_path = AssetDatabase.GetAssetPath (target);
		m_skin = Resources.Load ("Editor Skins/CustomSkin", typeof(GUISkin)) as GUISkin;
	}
		

	public override void OnInspectorGUI ()
	{
		
		if ( !AssetDatabase.IsValidFolder( m_path ) )
		{
			return;
		}

		if (m_skin == null) 
		{
			Empowerlabs.Utils.CustomTools.LogMessage ("CustomSkin not found in \"Resources/Editor Skins\"");
			DrawDefaultInspector ();
			return;
		}
		GUI.enabled = true;

		EditorGUILayout.BeginVertical (m_skin.GetStyle("window"));

		string directoryName = Path.GetFileName (m_path);

		EditorGUILayout.LabelField (directoryName, m_skin.GetStyle ("Header"));
		EditorGUILayout.LabelField ("", m_skin.GetStyle ("tooltip"));
		GUILayout.Space (1);

		EditorGUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Create Folder", m_skin.GetStyle ("button"))) 
		{
			EditorApplication.ExecuteMenuItem ("Assets/Create/Folder");
		}

		//Scenes Directory
		if (directoryName.Equals ("Scenes")) 
		{
			if (GUILayout.Button ("Create New Scene", m_skin.GetStyle ("button"))) 
			{
				EditorApplication.ExecuteMenuItem ("Assets/Create/Scene");
			}
		}

		//Scripts Directory
		if (directoryName.Equals ("Scripts")) 
		{
			if (GUILayout.Button ("Create Script", m_skin.GetStyle ("button"))) 
			{
				EditorApplication.ExecuteMenuItem ("Assets/Create/C# Script");
			}
		}

		//Prefab Directory
		if (directoryName.Equals ("Prefabs")) 
		{
			if (GUILayout.Button ("Create Prefab", m_skin.GetStyle ("button"))) 
			{
				EditorApplication.ExecuteMenuItem ("Assets/Create/Prefab");
			}
		}

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Import", m_skin.GetStyle ("button"))) 
		{
			EditorApplication.ExecuteMenuItem ("Assets/Import New Asset...");
		}

		if (GUILayout.Button ("Export", m_skin.GetStyle ("button"))) 
		{
			EditorApplication.ExecuteMenuItem ("Assets/Export Package...");
		}

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.EndVertical ();
	}
}

