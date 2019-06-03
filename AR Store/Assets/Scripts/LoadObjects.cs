using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;
using UnityEngine.UI;

public class LoadObjects : MonoBehaviour
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
        rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        Debug.Log(headers.ToString());
        UnityWebRequest readingsite = UnityWebRequest.Get("http://192.168.43.164:3000/api/products");
        readingsite.SetRequestHeader("Content-Type", "application/json");
        readingsite.method = "GET";
        yield return readingsite.Send();
        while (!readingsite.isDone)
            yield return null;
        if (string.IsNullOrEmpty(readingsite.error))
        {
            Debug.Log("Crash");
            JsonDataString = "{\"Products\":" + readingsite.downloadHandler.text + "}";
            Debug.Log(JsonDataString);
        }
        var StatusCode = readingsite.responseCode;

        Debug.Log("Return code: " + StatusCode);

        ProductListObject products = Processjson(JsonDataString);
        Debug.Log(products.Products.Count);
        WWW wwwww = new WWW("https://pp.userapi.com/c851028/v851028260/113022/mhaNmMSAw9M.jpg");

        yield return wwwww;
        foreach (var item in products.Products)
        {
            WWW www = new WWW(item.model);
            while (!www.isDone)
                yield return null;
            Debug.Log(www.texture.width);
            Debug.Log(www.texture.height);
            Texture2D textWebPic = www.texture;
            Debug.Log("ddd " + textWebPic.height);
            Sprite = Sprite.Create(textWebPic, new Rect(0, 0, textWebPic.width, textWebPic.height), new Vector2(0, 0));
            Debug.Log(item.name);
            Generate(item.name, Sprite);
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
    private ProductListObject Processjson(string jsonString)
    {
        Debug.Log("deserialaze " + jsonString);

        return JsonUtility.FromJson<ProductListObject>(jsonString);        
    }
    void Generate(string name, Sprite image)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform, false);
        scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text = name;
        scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = image;

    }
}

[Serializable]
public class Product
{
    public int id;

    public string name;

    public string model;

    public string cost;

    public string description;

    public bool isRotated;

    public int categoryId;

    public string imageForTarget;

    public float height;

    public float width;

    public float distance;

    public List<string> texturesObj;

}

[Serializable]
public class ProductListObject
{
    [SerializeField]
    public List<Product> Products;
}
public class Category
{
    public int Id;

    public string Name;

    public string Image;
}
