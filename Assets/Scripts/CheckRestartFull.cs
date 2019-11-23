using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRestartFull : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameController.GetInstance().Level1();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
