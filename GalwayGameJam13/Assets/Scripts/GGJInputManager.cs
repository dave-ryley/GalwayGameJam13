﻿using UnityEngine;
using System.Collections;

public class GGJInputManager : MonoBehaviour
{
    
    private string [] _inputs = new string[]
    {
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "space"
    };

    void Update()
    {
        for(int i = 0; i < _inputs.Length; i++)
        {
            if(Input.GetKeyDown(_inputs[i]))
            {
                GGJGameManager.HandleKeyDown(_inputs[i]);
            }
            if(Input.GetKey(_inputs[i]))
            {
                GGJGameManager.HandleKeyHold(_inputs[i]);
            }
            if(Input.GetKeyUp(_inputs[i]))
            {
                GGJGameManager.HandleKeyUp(_inputs[i]);
            }
        }
    }

#region Public members

    public void Setup()
    {

    }

#endregion
}
