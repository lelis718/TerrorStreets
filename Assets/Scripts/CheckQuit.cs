using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnMouseDown()
    {
        GameController.GetInstance().Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
