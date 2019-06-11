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
    public void OpenNewScene(string name)
    {
        Application.LoadLevel(name);
    }
}
