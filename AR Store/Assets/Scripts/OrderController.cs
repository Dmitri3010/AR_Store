﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    public InputField name, phone, adress;
    public int ProductId;
    public GameObject okPanel, ErrorPanel;


    public void SendRequest()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        Debug.Log("start sending....");
        WWWForm form = new WWWForm();
        form.AddField("Name", name.text);
        form.AddField("Phone", phone.text);
        form.AddField("Adress", adress.text);
        form.AddField("ProductId", ProductId);

        UnityWebRequest www = UnityWebRequest.Post("http://arstore.by/api/orders", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            ErrorPanel.active = true;
        }
        else
        {
            Debug.Log("Form upload complete!");
            okPanel.active = true;

        }
    }
}
