using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private Text scoreUI;
    private Player player;
    public void HandleInput(string key)
    {

    }

    public void OnStateEnter()
    {
        scoreUI = GameObject.FindObjectOfType<Text>();
        scoreUI.text = player.name + ": " + player.score;
        GGJGameManager.SetMusic(true);
    }

    public void OnStateExit()
    {
        GGJGameManager.SetMusic(false);
    }
}
