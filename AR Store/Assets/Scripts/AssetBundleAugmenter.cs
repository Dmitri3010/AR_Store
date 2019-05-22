using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Vuforia;
using Lean.Touch;

public class AssetBundleAugmenter : MonoBehaviour, ITrackableEventHandler
{

    public string AssetName;
    public int Version;

    private GameObject mBundleInstance = null;

    private TrackableBehaviour mTrackableBehaviour;

    private bool mAttached = false;

    void Start()
    {
        StartCoroutine(DownloadAndCache());

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }


    // Update is called once per frame
    IEnumerator DownloadAndCache()
    {
        while (!Caching.ready)
            yield return null;

        // example URL of file on PC filesystem (Windows)
        //string bundleURL = "file:///D:/Unity/AssetBundles/MyAssetBundle.unity3d";

        // example URL of file on Android device SD-card
        string bundleURL = "ftp://vozimru:550L[]linuxme@5.45.83.232/tolkan";

        using (WWW www = WWW.LoadFromCacheOrDownload(bundleURL, Version))
        {
            yield return www;

            if (www.error != null)
                throw new UnityException("WWW download had an error: " + www.error);

            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
            {
                mBundleInstance = Instantiate(bundle.mainAsset) as GameObject;
            }
            else
            {
                var assetLoadRequest = bundle.LoadAssetAsync<GameObject>("test");
                mBundleInstance = Instantiate((assetLoadRequest.asset as GameObject));
            }
        }
    }

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
}