using UnityEngine;
using UnityEngine.SceneManagement;

public class ArController : MonoBehaviour
{
    public GameObject likeButton, palitreButton, colors;
    public bool IsKlickedColors;
    
    void Start()
    {
        colors.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorsButton()
    {
        if (!IsKlickedColors)
        {
            colors.active = true;
            IsKlickedColors = true;
        }
        else
        {
            colors.active = false;
            IsKlickedColors = false;
        }
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }
}
