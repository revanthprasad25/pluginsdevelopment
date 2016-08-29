///Created on 29/8/2016
///B REVANTH PRASAD

///-----------------------------------------------------------------------------------------------
///Tool to create Asset Bundle
///-----------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;
using Empowerlabs.Utils;
using System;

namespace Empowerlabs.Editor
{
	public class AssetbundleCreator : EditorWindow
	{
		#region Variables
		private static AssetbundleCreator curWindow;
		private BuildTarget m_buildTarget = BuildTarget.Android;
		private BuildAssetBundleOptions m_options = BuildAssetBundleOptions.None;
		private bool m_isAssetFolderCreated;
		#endregion

	    #region MenuItems
	    [MenuItem("Empowerlabs/Tools/Asset Bundle Tools/Build Bundle")]
		private static void InitWindow()
	    {
			curWindow = EditorWindow.GetWindow<AssetbundleCreator> () as AssetbundleCreator;
			curWindow.minSize = new Vector2(400, 200);
			curWindow.maxSize = new Vector2(400, 210);
			curWindow.position = new Rect (new Vector2 (Screen.width / 2, Screen.height / 2), new Vector2 (400, 200));

			GUIContent content = new GUIContent();
			content.text = "Bundle Creator";
			curWindow.titleContent = content;
	    }
	    #endregion

		#region UnityEngine Methods
		private void OnGUI()
		{
			GUILayout.Space (10);

			EditorGUILayout.BeginVertical ();

			EditorGUILayout.LabelField ("Create a folder : \"Assets/Asset Bundle\"");
				
			GUILayout.Space (10);

			m_options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup ("Bundle Options", m_options);

			GUILayout.Space (10);

			m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ("Build Target", m_buildTarget);

			GUILayout.Space (10);

			if (GUILayout.Button ("Build")) 
			{
				if (EditorUtility.DisplayDialog ("Dude!", "Are you sure , you to want to build Asset bundle \nOptions\t:" + m_options.ToString () + "\nTarget\t:" + m_buildTarget.ToString (), "Yep", "Nope"))
				{
					Build ();
				}
			}

			EditorGUILayout.EndVertical ();
		}
		#endregion

		#region Methods
		private void Build()
		{
			if (BuildPipeline.BuildAssetBundles ("Assets/Asset Bundle", m_options, m_buildTarget)) 
			{
				CustomTools.LogMessage ("Asset Bundle Created");
				string[] names = AssetDatabase.GetAllAssetBundleNames ();

				for (int i = 0; i < names.Length; i++)
					CustomTools.LogMessage (names [i]);
			} 
			else 
			{
				AssetDatabase.CreateFolder ("Assets", "Asset Bundle");
				Build ();
			}
		}
		#endregion
	}
}