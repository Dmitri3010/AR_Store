using UnityEngine;

public class ShowCart : MonoBehaviour
{
    public GameObject cartCanvas, ArCanvas, imageTarget;
   public void Show()
    {
        cartCanvas.active = true;
        ArCanvas.active = false;
        imageTarget.active = false;

    }
}
