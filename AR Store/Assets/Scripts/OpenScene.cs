﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    public void OpenNewScene(string naffme)
    {
        Application.LoadLevel(name);
    }
}