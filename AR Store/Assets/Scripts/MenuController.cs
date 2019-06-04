using UnityEngine;

public class MenuController : MonoBehaviour
{
    public float moveSpeed = 1400;

    private Vector2 startPos;
    private Vector2 target;
    private bool IsOpen = false;
    public GameObject Canvas;


    void Start()
    {
        
    }

    public void MenuControll()
    {
        if (IsOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    void Update()
    {
    }

    public void OpenMenu()
    {
        Canvas.SetActive(true);
        Debug.Log("Open Menu");
        IsOpen = true;
    }

    public  void CloseMenu()
    {
        Canvas.SetActive(false);
        Debug.Log("hide menu");
        IsOpen = false;
    }
}
