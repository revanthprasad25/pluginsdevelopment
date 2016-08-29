using UnityEngine;
using UnityEditor;
using Empowerlabs.Utils;

namespace Empowerlabs.Editor
{
	public class AssetBundlePostProcessor : AssetPostprocessor
	{
		public void OnPostprocessAssetbundleNameChanged( string assetPath, string previousAssetBundleName, string newAssetBundleName)
		{
			CustomTools.LogMessage("Asset " + assetPath + " has been moved from assetBundle " + previousAssetBundleName + " to assetBundle " + newAssetBundleName + ".");
		}

//		private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//		{
//			System.Text.StringBuilder importedAssetString = new System.Text.StringBuilder ();
//
//			importedAssetString.Append ("Reimported Asset: \n");
//			for (int i = 0; i < importedAssets.Length; i++)
//			{
//				importedAssetString.Append (importedAssets [i]);
//				importedAssetString.Append ("\n");
//			}
//
//			importedAssetString.Append ("Deleted Asset: \n");
//			for (int i = 0; i < deletedAssets.Length; i++) 
//			{
//				importedAssetString.Append (deletedAssets [i]);
//				importedAssetString.Append ("\n");
//			}
//
//			importedAssetString.Append ("Moved Asset: \n");
//			for (int i=0; i<movedAssets.Length; i++)
//			{
//				importedAssetString.Append ("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
//				importedAssetString.Append ("\n");
//			}
//
//			CustomTools.LogMessage (importedAssetString);
//		}

		private void OnPreprocessTexture ()
		{
			TextureImporter m_texImport = (TextureImporter)assetImporter;
			m_texImport.textureType = TextureImporterType.Sprite;
			m_texImport.mipmapEnabled = false;
			m_texImport.anisoLevel = 0;
			m_texImport.filterMode = FilterMode.Bilinear;

			if (m_texImport.normalmap) 
			{
				m_texImport.textureType = TextureImporterType.Bump;
				m_texImport.grayscaleToAlpha = false;
			}
		}
	}
}