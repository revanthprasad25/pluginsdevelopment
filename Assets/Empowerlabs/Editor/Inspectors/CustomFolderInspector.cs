using UnityEngine;
using UnityEditor;
using System.Collections;

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
		GUI.enabled = true;
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

		EditorGUILayout.BeginHorizontal ();
	
		EditorGUILayout.EndVertical ();
	}

	private void ResourceHierarchy()
	{
		
	}
}
