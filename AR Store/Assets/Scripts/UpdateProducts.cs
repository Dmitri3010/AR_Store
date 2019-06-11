using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UpdateProducts : MonoBehaviour
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
    public int categoryId;
    // Start is called before the first frame update
   

    [System.Obsolete]
    public IEnumerable Load()
    {
        rectComponent = GameObject.Find("Loading Circle").GetComponent<RectTransform>();
        UnityWebRequest readingsite = UnityWebRequest.Get("http://arstore.by/api/products");
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

        foreach (Transform child in scrollItemPrefab.transform)
        {
            GameObject.Destroy(child.gameObject);
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
    }

    public void LoadProduct()
    {
        Debug.Log("Start load new products");
        Load();
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
