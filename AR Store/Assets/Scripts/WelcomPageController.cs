using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomPageController : MonoBehaviour
{
    public GameObject first, second, threed;
    public GameObject spiner;
    private RectTransform rectComponent;
    private float rotateSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        second.active = false;
        threed.active = false;
        rectComponent = spiner.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void OpenSecond()
    {
        first.active = false;
        second.active = true;
        threed.active = false;
    }

    public void OpenFirst()
    {
        first.active = true;
        second.active = false;
        threed.active = false;
    }

    public void OpenThreed()
    {
        first.active = false;
        second.active = false;
        threed.active = true;
    }

    public void OpenMainSchene()
    {
        spiner.active = true;
        RotateSpiner();
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }

    IEnumerator RotateSpiner()
    {
        yield return new WaitForSeconds(5);
    }
}
