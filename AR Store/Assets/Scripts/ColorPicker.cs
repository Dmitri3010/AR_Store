using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public Material[] bodyMaterial;
    Material currentMaterial;
    new Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedColor()
    {
        render.material = bodyMaterial[1];
        currentMaterial = render.material;
    }
    public void BlueColor()
    {
        render.material = bodyMaterial[2];
        currentMaterial = render.material;
    }

    public void StandartColor()
    {
        render.material = bodyMaterial[0];
        currentMaterial = render.material;
    }
}
