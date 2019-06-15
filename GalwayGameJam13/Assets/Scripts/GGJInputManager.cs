﻿using UnityEngine;
using System.Collections;

public class GGJInputManager : MonoBehaviour
{
    private string [] _inputs = new string[]
    {
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m"
    };

    private GGJGameManager _gameManager;

    void Update()
    {
        if(_gameManager == null) return;
        for(int i = 0; i < _inputs.Length; i++)
        {
            if(Input.GetKeyDown(_inputs[i]))
            {
                _gameManager.HandleInput(_inputs[i]);
            }
        }
    }

#region Public members

    public void Setup(GGJGameManager gameManager)
    {
        _gameManager = gameManager;
    }

#endregion
}