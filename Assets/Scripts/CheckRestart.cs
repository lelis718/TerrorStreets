﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        GameController.GetInstance().Restart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
