using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{

    public string BundleURL;
    public string AssetName;
    public int version;

    
        IEnumerator Start()
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine("D:\\Download", "Chair.unity3d"));
            yield return bundleLoadRequest;

            var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
            Debug.Log("load AssetBundle!");
            var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("Chair");
            yield return assetLoadRequest;

            GameObject prefab = assetLoadRequest.asset as GameObject;
            Instantiate(prefab);

            myLoadedAssetBundle.Unload(false);
        }


    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
                Instantiate(bundle.mainAsset);
            else
                Instantiate(bundle.LoadAsset(AssetName));
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    string url = "https://localhost:44375/api/model?productId=2";
    //    WWW www = new WWW(url);
    //    StartCoroutine(WaitForReq(www));
    //}

    //IEnumerator WaitForReq(WWW www)
    //{
    //    yield return www;
    //    AssetBundle bundle = www.assetBundle;
    //    if (www.error == null)
    //    {
    //        GameObject wheel = (GameObject)bundle.LoadAsset("chair");
    //        Instantiate(wheel); // **Change its position and rotation 
    //        Debug.Log("load chair");
    //    }
    //    else
    //    {
    //        Debug.Log(www.error);
    //        Debug.Log("kek");
    //    }
    //}
}
