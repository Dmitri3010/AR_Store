using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using SimpleJSON;


public class LoadObjects : MonoBehaviour
{
    string JsonDataString;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW readingsite = new WWW("https://vozim.ru/test.json");
        yield return readingsite;

        if (string.IsNullOrEmpty(readingsite.error))
        {
            JsonDataString = readingsite.text;
        }

        Debug.Log(JsonDataString);

        Processjson(JsonDataString);
        //JSONNode jsonNode = SimpleJSON.JSON.Parse(JsonDataString);

        //CountryName.text = jsonNode["name"].ToString().ToUpper();
        //Debug.Log(jsonNode["name"]);
        //        string url = "https://localhost:44375/api/products";
        //        UnityWebRequest uwr = UnityWebRequest.Get(url);
        //        yield return uwr.SendWebRequest();

        //        if (uwr.isNetworkError)
        //        {
        //            Debug.Log("Error While Sending: " + uwr.error);
        //        }
        //        else
        //        {
        //            Debug.Log("Received: " + uwr.downloadHandler.text);
        //        }
        //        string data1 = "Текст 1";
        //        string data2 = "Текст 2";
        //        WWW Query = new WWW(url);
        //        yield return Query;
        //        if (Query.error != null)
        //        {
        //            Debug.Log("Server does not respond : " + Query.error);
        //        }
        //        else
        //        {
        //            if (Query.text == "response") // что нам должен ответить сервер на наши данные
        //            {
        //                Debug.Log("Server responded correctly");
        //            }
        //            else
        //            {
        //                Debug.Log("Server responded : " + Query.text);
        //            }
        //        }
        //        Query.Dispose();


        //WWW www = new WWW(url);
        //        yield return www;
        //        if (www.error == null)
        //        {
        //            Debug.Log("ok ");
        //        }
        //        else
        //        {
        //            Debug.Log("ERROR: pizdec " + www.error);
        //        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Processjson(string jsonString)
    {
        List<Product> products = new List<Product>();
        products = JsonUtility.FromJson<List<Product>>(jsonString);
    }
}


[Serializable]
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Model { get; set; }

    public string Cost { get; set; }

    public string Description { get; set; }

    public bool IsRotated { get; set; }

    public int CategoryId { get; set; }

    public string ImageForTarget { get; set; }

    public float Height { get; set; }

    public float Width { get; set; }

    public float Distance { get; set; }

    //public string JServices
    //{
    //    get => texturesObj != null ? JsonConvert.SerializeObject(texturesObj) : string.Empty;
    //    set => texturesObj = JsonConvert.DeserializeObject<List<string>>(value ?? string.Empty);
    //}

    public List<string> texturesObj { get; set; }

    public virtual Category Category { get; set; }
}

[System.Serializable]
public class WeaponListObject
{

    public List<Product> products;
}
public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
}
