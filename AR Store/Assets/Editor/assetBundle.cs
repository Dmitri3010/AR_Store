using UnityEditor;

public class assetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetsBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundle", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
