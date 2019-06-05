using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
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
    public GameObject SingleProductCost, SingleProductTest, SingleProductImage, SingleProductName, SingleProductSize;
    public List<Product> listOfProducts;
    public GameObject MainObj, SingleObj;

    // Start is called before the first frame update
    [Obsolete]
    IEnumerator Start()
    {
        MainObj.active = true;
        SingleObj.active = false;
        rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        Debug.Log(headers.ToString());
        UnityWebRequest readingsite = UnityWebRequest.Get("http://localhost:5000/api/products");
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
            item.sprite = Sprite;
            listOfProducts.Add(item);
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
    public ProductListObject Processjson(string jsonString)
    {
        Debug.Log("deserialaze " + jsonString);

        return JsonUtility.FromJson<ProductListObject>(jsonString);
    }
    public void Generate(string name, Sprite image)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform, false);
        scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text = name;
        scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = image;
        scrollItemObj.GetComponent<Button>().onClick.AddListener((delegate { OpenSingleProduct(scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text); }));

    }

    public void OpenSingleProduct(string name)
    {
        foreach (var prod in listOfProducts)
        {
            if (prod.name == name)
            {
                SingleProductTest.GetComponent<Text>().text = prod.description;
                SingleProductCost.GetComponent<Text>().text = prod.cost;
                SingleProductImage.GetComponent<Image>().sprite = prod.sprite;
                SingleProductName.GetComponent<Text>().text = prod.name;
                SingleProductSize.GetComponent<Text>().text = $"{prod.height} * {prod.width} * {prod.distance}";
            }
        }
        MainObj.active = false;
        SingleObj.active = true;

    }

    public void ReturnToMain()
    {
        MainObj.active = true;
        SingleObj.active = false;
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

    public Texture2D image;

    public Sprite sprite;

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
