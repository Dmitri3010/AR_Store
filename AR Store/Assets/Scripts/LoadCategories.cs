using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadCategories : MonoBehaviour
{
    string JsonDataString;
    private Image img;
    private Sprite Sprite;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    private RectTransform rectComponent;
    private float rotateSpeed = 200f;
    public bool loading;
    public GameObject spiner;

    // Start is called before the first frame update
    [Obsolete]
    IEnumerator Start()
    {
        // rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        Debug.Log(headers.ToString());
        UnityWebRequest readingsite = UnityWebRequest.Get("http://192.168.43.164:3000/api/categories");
        readingsite.SetRequestHeader("Content-Type", "application/json");
        readingsite.method = "GET";
        yield return readingsite.Send();
        while (!readingsite.isDone)
            yield return null;
        if (string.IsNullOrEmpty(readingsite.error))
        {
            Debug.Log("Crash");
            JsonDataString = "{\"Categories\":" + readingsite.downloadHandler.text + "}";
            Debug.Log(JsonDataString);
        }
        var StatusCode = readingsite.responseCode;

        Debug.Log("Return code: " + StatusCode);

        CategoriesListObject categories = Processjson(JsonDataString);
        Debug.Log("Count categories: "+categories.Categories.Count);       
        foreach (var item in categories.Categories)
        {            
            Debug.Log(item.name);
            Generate(item.name);
        }
        loading = true;
        spiner.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loading)
            rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
    private CategoriesListObject Processjson(string jsonString)
    {
        Debug.Log("deserialaze " + jsonString);

        return JsonUtility.FromJson<CategoriesListObject>(jsonString);
    }
    void Generate(string name)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform, false);
        scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text = name;

    }
    
    [Serializable]
    public class CategoriesListObject
    {
        [SerializeField]
        public List<Category> Categories;
    }
    [Serializable]
    public class Category
    {
        public int id;

        public string name;

        public string image;
    }
}
