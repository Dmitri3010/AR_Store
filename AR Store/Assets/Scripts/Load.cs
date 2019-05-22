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
                //Instantiate(bundle.LoadAsset(AssetName));
                var assetLoadRequest = bundle.LoadAssetAsync<GameObject>("test");
                yield return assetLoadRequest;

                Instantiate(assetLoadRequest.asset as GameObject, imageTarget.transform);
                //GameObject prefab = assetLoadRequest.asset as GameObject;
                //prefab.transform.position = model.transform.position;
                //prefab.transform.SetParent(imageTarget.transform);
                //Instantiate(prefab);
                Debug.Log("js pidor");
            }
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}

    //void Start()
    //{
    //    string url = "https://localhost:44375/api/model?productId=3";
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

//void Start()
//{
//    StartCoroutine(GetAssetBundle());
//}

//IEnumerator GetAssetBundle()
//{
//    string url = "https://volafile.org/get/CR-UeyvlhMI_p/chair%20(1).unity3d";

//    UnityWebRequest www = UnityWebRequest.Get(url);
//    DownloadHandler handle = www.downloadHandler;

//    //Send Request and wait
//    yield return www.Send();

//    if (www.isError)
//    {

//        UnityEngine.Debug.Log("Error while Downloading Data: " + www.error);
//    }
//    else
//    {
//        UnityEngine.Debug.Log("Success");

//handle.data

////Construct path to save it
//string dataFileName = "WaterVehicles";
//string tempPath = Path.Combine(Application.persistentDataPath, "AssetData");
//tempPath = Path.Combine(tempPath, dataFileName + ".unity3d");

//            ////Save
//            //save(handle.data, tempPath);
//        }
//        //UnityWebRequest www = UnityWebRequest.GetAssetBundle("https://localhost:44375/api/model?productId=3");
//        //yield return www.SendWebRequest();

//        //if (www.isNetworkError || www.isHttpError)
//        //{
//        //    Debug.Log("error: "+www.error);
//        //}
//        //else
//        //{
//        //    Debug.Log("ok");
//        //    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
//        //    Instantiate(bundle.LoadAsset(AssetName));
//        //}
//    }
//}
