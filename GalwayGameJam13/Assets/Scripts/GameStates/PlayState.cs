using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IGameState
{
    public void HandleInput(string key)
    {
        Player player;
        if(GGJGameManager.TryGetPlayer(key, out player))
        {
            player.Jump();
        }
    }

    public void OnStateEnter()
    {

    }

    public void OnStateExit()
    {

    }
}
