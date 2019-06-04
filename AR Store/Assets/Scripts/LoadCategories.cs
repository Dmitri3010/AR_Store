using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadCategories : MonoBehaviour
{
    public int categoryId;
    string JsonDataString;
    private Image img;
    private Sprite Sprite;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    private float rotateSpeed = 200f;
    public GameObject scrollItemPrefab;
    private RectTransform rectComponent;
    public ScrollRect productsscrollView;
    public GameObject productsscrollContent;
    public GameObject productsscrollItemPrefab;
    public bool loading;
    public GameObject spiner;
    private bool isRunning = false;
    public List<Product> listOfProducts;



    // Start is called before the first frame update
    [Obsolete]
    IEnumerator Start()
    {
        // rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        Debug.Log(headers.ToString());
        UnityWebRequest readingsite = UnityWebRequest.Get("http://localhost:5000/api/categories");
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
        Debug.Log("Count categories: " + categories.Categories.Count);
        foreach (var item in categories.Categories)
        {

            Debug.Log(item.name);
            Generate(item.name);
        }
        UnityWebRequest readingprod = UnityWebRequest.Get("http://localhost:5000/api/products");
        readingprod.SetRequestHeader("Content-Type", "application/json");
        readingprod.method = "GET";
        yield return readingprod.Send();
        while (!readingprod.isDone)
            yield return null;
        if (string.IsNullOrEmpty(readingprod.error))
        {
            Debug.Log("Crash");
            JsonDataString = "{\"Products\":" + readingprod.downloadHandler.text + "}";
            Debug.Log(JsonDataString);
        }

        Debug.Log("Return code: " + StatusCode);

        ProductListObject products = Processprodjson(JsonDataString);
        Debug.Log(products.Products.Count);

        foreach (var item in products.Products)
        {
            WWW www = new WWW(item.model);
            while (!www.isDone)
                yield return null;
            Texture2D textWebPic = www.texture;
            item.image = textWebPic;
            listOfProducts.Add(item);
        }
        Debug.Log("Count of products" + listOfProducts.Count);
        loading = true;
        spiner.active = false;
    }
    public ProductListObject Processprodjson(string jsonString)
    {
        Debug.Log("deserialaze " + jsonString);

        return JsonUtility.FromJson<ProductListObject>(jsonString);
    }

    [Obsolete]
    public void StartDownload()
    {
        Debug.Log("Lol");
        foreach (var item in listOfProducts)
        {
            if (item.categoryId == categoryId)
            {
                
                Sprite = Sprite.Create(item.image, new Rect(0, 0, item.image.width, item.image.height), new Vector2(0, 0));
                Debug.Log(item.name);
                Generate(item.name, Sprite);
            }

        }
        foreach (Transform child in productsscrollContent.transform)
        {
            GameObject.DestroyImmediate(child.gameObject, true);
        }

        loading = true;
        spiner.active = false;
    }

    [Obsolete]
    public IEnumerable Load()
    {
        isRunning = true;

        Debug.Log("Click");
        rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
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

        ProductListObject products = ProcessProductjson(JsonDataString);
        Debug.Log(products.Products.Count);

        foreach (Transform child in scrollItemPrefab.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        foreach (var item in products.Products)
        {
            if (item.categoryId == categoryId)
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

        }

        loading = true;
        spiner.active = false;
        yield return null;
        isRunning = false;

    }

    public void LoadProduct()
    {
        Debug.Log("Start load new products");
        Load();
    }
    private ProductListObject ProcessProductjson(string jsonString)
    {
        Debug.Log("deserialaze " + jsonString);

        return JsonUtility.FromJson<ProductListObject>(jsonString);
    }
    void Generate(string name, Sprite image)
    {
        GameObject scrollItemObj = Instantiate(productsscrollItemPrefab);
        scrollItemObj.transform.SetParent(productsscrollContent.transform, false);
        scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text = name;
        scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = image;

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

        scrollItemObj.GetComponent<Button>().onClick.AddListener((delegate { StartDownload(); }));
        Debug.Log("pizda" + scrollItemObj.GetComponent<Button>().name);
        //scrollItemObj.transform.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Load(); });
        //scrollItemObj.transform.Find("Text").gameObject.GetComponent<EventTrigger>().triggers.Add(entry);


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
