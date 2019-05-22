using UnityEngine;
using UnityEditor;

public class assetBundle
{
    [MenuItem("Assets/Build AssetBundle From Selection - Track dependencies")]
    [System.Obsolete]
    static void ExportResurce()
    {
        // Bring up save panel
        string basename = Selection.activeObject ? Selection.activeObject.name : "New Resource";
        string path = EditorUtility.SaveFilePanel("Save Resources", "", basename, "");

        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            // for Android
            BuildPipeline.BuildAssetBundle(Selection.activeObject,
                                           selection, path + ".android.unity3d",
                                           BuildAssetBundleOptions.CollectDependencies |
                                           BuildAssetBundleOptions.CompleteAssets,
                                           BuildTarget.Android);

            //// for iPhone
            //BuildPipeline.BuildAssetBundle(Selection.activeObject,
            //                               selection, path + ".iphone.unity3d",
            //                               BuildAssetBundleOptions.CollectDependencies |
            //                               BuildAssetBundleOptions.CompleteAssets,
            //                               BuildTarget.iPhone);

            //// for WebPlayer
            //BuildPipeline.BuildAssetBundle(Selection.activeObject,
            //                               selection, path + ".unity3d",
            //                               BuildAssetBundleOptions.CollectDependencies |
            //                               BuildAssetBundleOptions.CompleteAssets,
            //                               BuildTarget.WebPlayer);

            Selection.objects = selection;
        }
    }
}
