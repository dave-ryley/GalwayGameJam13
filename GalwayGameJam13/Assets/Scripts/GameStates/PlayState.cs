using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private Text scoreUI;
    //private GameObject _playerPrefab;

    public PlayState()
    {
        //_playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
    }


    public void HandleInput(string key)
    {

    }

    public void OnStateEnter()
    {
        scoreUI = Object.FindObjectOfType<Text>();
        List<string> names = GGJGameManager.GetPlayerNames();
        Debug.Log("names.Count = " + names.Count);
        scoreUI.text = "";
        for (int i = 0; i < names.Count; i++)
        {
            Player player;
            GGJGameManager.TryGetPlayer(names[i], out player);
            scoreUI.text += names[i] + ": " + player.score + "\t";
        }  
        
        GGJGameManager.SetMusic(true);
        
    }

    public void OnStateExit()
    {
        GGJGameManager.SetMusic(false);
    }
}
