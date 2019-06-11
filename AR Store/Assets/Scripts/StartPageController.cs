using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartPageController : MonoBehaviour
{
    public GameObject spiner, checktext, aboutBth, startbth;
    private RectTransform rectComponent;
    private float rotateSpeed = 200f;
    public bool loading;
    IEnumerator Start()
    {
        spiner.active = true;
        rectComponent = GameObject.Find("spiner").GetComponent<RectTransform>();
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        Debug.Log(headers.ToString());
        UnityWebRequest readingsite = UnityWebRequest.Get("http://arstore.by/api/categories");
        readingsite.SetRequestHeader("Content-Type", "application/json");
        readingsite.method = "GET";
        yield return readingsite.Send();
        yield return new WaitForSeconds(5);
        while (!readingsite.isDone)
            yield return null;
        if (string.IsNullOrEmpty(readingsite.error))
        {
            spiner.active = false;
            startbth.active = true;
            aboutBth.active = true;
            checktext.active = false;

        }
        var StatusCode = readingsite.responseCode;

        Debug.Log("Return code: " + StatusCode);

      
        loading = true;
       

    }
    // Update is called once per frame
    void Update()
    {
        if (!loading)
            rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void OpenWelcomePage()
    {
        SceneManager.LoadScene("WelcomeScreen", LoadSceneMode.Single);
    }

    public void OpenMainPage()
    {
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }
}
