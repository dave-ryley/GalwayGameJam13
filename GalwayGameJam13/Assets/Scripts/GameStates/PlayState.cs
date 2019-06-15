using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IGameState
{
    public void HandleInput(string key)
    {

    }

    public void OnStateEnter()
    {
        GGJGameManager.SetMusic(true);
    }

    public void OnStateExit()
    {
        GGJGameManager.SetMusic(false);
    }
}
