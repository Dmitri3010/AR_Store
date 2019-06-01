using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite[] s1;
    public Button button;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //s1 = Resources.LoadAll<Sprite>("UI element like");
    }

    public void On_button_Click()
    {
        count++;
        if(count == s1.Length)
        {
            count = 0;
        }

        button.image.sprite = s1[count];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
