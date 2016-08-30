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
		private GUISkin m_skin;
		#endregion

	    #region MenuItems
	    [MenuItem("Empowerlabs/Tools/Asset Bundle Tools/Build Bundle")]
		private static void InitWindow()
	    {
			curWindow = EditorWindow.GetWindow<AssetbundleCreator> () as AssetbundleCreator;
			curWindow.minSize = new Vector2(400, 300);
			curWindow.maxSize = new Vector2(400, 300);
			curWindow.position = new Rect (new Vector2 (Screen.width / 2, Screen.height / 2), new Vector2 (400, 200));
			curWindow.ShowUtility ();

			GUIContent content = new GUIContent();
			content.tooltip = "Tool to create Asset Bundle";
			content.text = "Bundle Creator";
			curWindow.titleContent = content;
	    }
	    #endregion

		#region UnityEngine Methods
		private void OnEnable()
		{
			m_skin = Resources.Load ("Editor Skins/CustomSkin", typeof(GUISkin)) as GUISkin;
		}


		private void OnGUI()
		{
			EditorGUILayout.BeginVertical (m_skin.GetStyle("window"));

			EditorGUILayout.LabelField ("Asset Bundle Tool", m_skin.GetStyle ("header"));
			EditorGUILayout.LabelField ("Tool to create Asset Bundle", m_skin.GetStyle ("tooltip"));

			GUILayout.Space (10);

			EditorGUILayout.BeginVertical ();
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.LabelField ("Bundle Options" , m_skin.GetStyle("label"));
			m_options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup ( m_options , m_skin.GetStyle("button"));

			EditorGUILayout.EndHorizontal ();
			GUILayout.Space (10);

			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.LabelField ("Build Target" , m_skin.GetStyle("label"));
			m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ( m_buildTarget, m_skin.GetStyle ("button"));

			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.EndVertical ();

			GUILayout.Space (25);

			if (GUILayout.Button ("Click here to create Bundle folder" , m_skin.GetStyle("button")))
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

			if (GUILayout.Button ("Build" , m_skin.GetStyle("button"))) 
			{
				if (EditorUtility.DisplayDialog ("Alert", "Are you sure , you to want to build Asset bundle \nOptions\t:" + m_options.ToString () + "\nTarget\t:" + m_buildTarget.ToString (), "Yes", "No"))
				{
					Build ();
				}
			}

			EditorGUILayout.EndVertical ();
			Repaint ();
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