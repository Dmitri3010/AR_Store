using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Vuforia;
using Lean.Touch;
using System.IO;
using UnityEngine.UI;

public class AssetBundleAugmenter : MonoBehaviour, ITrackableEventHandler
{
    static string pathLOCAL;

    public string AssetName;
    public int Version;
    public Text text;
    private GameObject mBundleInstance = null;

    public GameObject model;

    private TrackableBehaviour mTrackableBehaviour;

    private bool mAttached = false;
    static WWW objSERVER;


    void Start()
    {

        mBundleInstance = model;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }


    // Update is called once per frame
    //IEnumerator DownloadAndCache()
    //{
    //    while (!Caching.ready)
    //        yield return null;

    //    // example URL of file on PC filesystem (Windows)
    //    //string bundleURL = "file:///D:/Unity/AssetBundles/MyAssetBundle.unity3d";
    //    pathLOCAL = Application.persistentDataPath + "/../assetbundles/" + "test" + ".unity3d"; //location of the file on the device

    //    // example URL of file on Android device SD-card
    //    string bundleURL = "ftp://vozimru:550L[]linuxme@5.45.83.232/tolkan";

    //   // LoadBundleFromServer(bundleURL, )

    //    using (WWW www = WWW.LoadFromCacheOrDownload(bundleURL, Version))
    //    {
    //        yield return www;

    //        if (www.error != null)
    //        {
    //           // text.text = www.error;

    //            throw new UnityException("WWW download had an error: " + www.error);
    //        }

    //        AssetBundle bundles = www.assetBundle;
    //        if (AssetName == "")
    //        {
    //            mBundleInstance = Instantiate(bundles.mainAsset) as GameObject;
    //        }
    //        else
    //        {
    //           // text.text ="OK!!!!!!!!!";

    //            var assetLoadRequest = bundles.LoadAssetAsync<GameObject>("test");
    //            mBundleInstance = Instantiate((assetLoadRequest.asset as GameObject));
    //        }

    //objSERVER =   new WWW(bundleURL);
    // yield return objSERVER;
    // if (AssetName == "")
    // {
    //     mBundleInstance = Instantiate(bundle.mainAsset) as GameObject;
    // }
    // else
    // {
    //     SaveDownloadedAsset(objSERVER);
    //     Debug.Log("------------------- HERE THE PATH -------------------" + "\n" + Application.persistentDataPath);

    //     AssetBundle objLOCAL = AssetBundle.LoadFromFile(pathLOCAL);
    //     yield return objLOCAL;

    //     var assetLoadRequest = objLOCAL.LoadAssetAsync<GameObject>("test");
    //     // Instantiate the asset bundle
    //     mBundleInstance = Instantiate((assetLoadRequest.asset as GameObject));

    // Parenting
    //Model3D.transform.parent = cible.transform;
    //using (WWW wwww = WWW.LoadFromCacheOrDownload(pathLOCAL, Version))
    //{
    //    yield return wwww;

    //    if (wwww.error != null)
    //        throw new UnityException("WWW download had an error: " + www.error);

    //    AssetBundle bundles = wwww.assetBundle;
    //    if (AssetName == "")
    //    {
    //        mBundleInstance = Instantiate(bundles.mainAsset) as GameObject;
    //    }
    //    else
    //    {
    //        var assetLoadRequest = bundles.LoadAssetAsync<GameObject>("test");
    //        mBundleInstance = Instantiate((assetLoadRequest.asset as GameObject));
    //    }
    //}




public void OnTrackableStateChanged(
    TrackableBehaviour.Status previousStatus,
    TrackableBehaviour.Status newStatus)
{
    if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
    {
        if (!mAttached && mBundleInstance)
        {
            // if bundle has been loaded, let's attach it to this trackable
            mBundleInstance.transform.parent = this.transform;
            mBundleInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mBundleInstance.transform.localPosition = new Vector3(0.0f, 0.15f, 0.0f);
            mBundleInstance.transform.gameObject.SetActive(true);
            mBundleInstance.AddComponent<LeanRotate>();
            mBundleInstance.AddComponent<LeanScale>();
            mBundleInstance.AddComponent<LeanTranslate>();
            BoxCollider boxCollider = mBundleInstance.AddComponent<BoxCollider>();

            mAttached = true;
        }
    }
}
    //public void SaveDownloadedAsset(WWW objSERVER)
    //{
    //    // Create the directory if it doesn't already exist
    //    if (!Directory.Exists(Application.persistentDataPath + "/../assetbundles/"))
    //    {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/../assetbundles/");
    //    }

    //    // Initialize the byte string
    //    byte[] bytes = objSERVER.bytes;

    //    // Creates a new file, writes the specified byte array to the file, and then closes the file. 
    //    // If the target file already exists, it is overwritten.
    //    File.WriteAllBytes(pathLOCAL, bytes);
    //}
    //IEnumerator LoadBundleFromServer(string url)
    //{
    //    var request = UnityWebRequestAssetBundle.GetAssetBundle(url);

    //    yield return request.SendWebRequest();

    //    if (!request.isHttpError && !request.isNetworkError)
    //    {
    //        var assetLoadRequest = DownloadHandlerAssetBundle.GetContent(uwr);
    //        mBundleInstance = Instantiate((assetLoadRequest.asset as GameObject));
    //        response(DownloadHandlerAssetBundle.GetContent(request));
    //        text.text = "OK!!!!!!!!!!!!!!!!!!!!";
    //    }
    //    else
    //    {
    //        text.text = request.error;
    //        Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

    //        response(null);
    //    }

    //    request.Dispose();
    //}
}

