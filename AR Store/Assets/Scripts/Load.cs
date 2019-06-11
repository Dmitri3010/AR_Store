using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Load : MonoBehaviour
{

    public string BundleURL;
    public string AssetName;
    public int version;
    public GameObject model, imageTarget;

    void Start()
    {
        StartCoroutine(DownloadAndCache());
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
            Debug.Log("Suck");
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
                Instantiate(bundle.mainAsset);
            else
            {
                var assetLoadRequest = bundle.LoadAssetAsync<GameObject>("test");
                yield return assetLoadRequest;

                Instantiate(assetLoadRequest.asset as GameObject, imageTarget.transform);
                
            }
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}   