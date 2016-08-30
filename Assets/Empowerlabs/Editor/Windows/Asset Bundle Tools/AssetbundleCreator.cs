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
		private int m_counter = 0;
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

			GUILayout.Space (10);

			m_options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup ("Bundle Options", m_options);

			GUILayout.Space (10);

			m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ("Build Target", m_buildTarget);

			GUILayout.Space (20);

			if (GUILayout.Button ("Click here to create Bundle folder"))
			{
				if (CheckForBundleFolder ()) {
					if (EditorUtility.DisplayDialog ("Info", "Folder is already created, Do you want to start building", "Build", "Cancel")) {
						if (EditorUtility.DisplayDialog ("Alert", "Are you sure , you to want to build Asset bundle \nOptions\t:" + m_options.ToString () + "\nTarget\t:" + m_buildTarget.ToString (), "Yes", "No")) {
							Build ();
						}
					}
				} else {
					AssetDatabase.CreateFolder ("Assets", "_AssetBundle");
				}
			}

			GUILayout.Space (10);

			if (GUILayout.Button ("Build")) 
			{
				if (EditorUtility.DisplayDialog ("Alert", "Are you sure , you to want to build Asset bundle \nOptions\t:" + m_options.ToString () + "\nTarget\t:" + m_buildTarget.ToString (), "Yes", "No"))
				{
					Build ();
				}
			}

			EditorGUILayout.EndVertical ();
		}
		#endregion

		#region Methods

		private bool CheckForBundleFolder()
		{
			string[] assetBundleFolder = AssetDatabase.GetSubFolders ("Assets");	//Gets all the subfolders in assets
			for (int i = 0; i < assetBundleFolder.Length; i++)
			{
				//Asset Bundle folder is already created
				if (assetBundleFolder [i].Contains ("_AssetBundle")) 
				{
					return true;
				}
			}

			return false;
		}


		private void Build()
		{
			if (BuildPipeline.BuildAssetBundles ("Assets/_AssetBundle", m_options, m_buildTarget)) 
			{
				CustomTools.LogMessage ("Asset Bundle Created");
				string[] names = AssetDatabase.GetAllAssetBundleNames ();
				for (int i = 0; i < names.Length; i++)
					CustomTools.LogMessage (names [i]);
			} 
			else 
			{
				EditorUtility.DisplayDialog ("Info", "Failed to create Assets Bundle \n 1. Atleast one asset should be under asset bundle \n 2. If you don't see \"_AssetBundle\" folder , please create under Assets", "OK");
			}
		}
		#endregion
	}
}