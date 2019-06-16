using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private GameObject TextPrefab;
    private GameObject _textPrefab;

    public PlayState()
    {
        TextPrefab = Resources.Load("Prefabs/ScoreUI") as GameObject;
    }


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
         var scoreLayouts = GameObject.FindGameObjectsWithTag("Score");
         List<string> names = GGJGameManager.GetPlayerNames();
         int j = 0;
         for (int i = 0; i < names.Count; i++)
         {
             if (scoreLayouts[j].transform.childCount > 9) { j++; }
             _textPrefab = Object.Instantiate(TextPrefab);
             Player player;
             GGJGameManager.TryGetPlayer(names[i], out player);
             _textPrefab.GetComponent<Text>().text =  names[i].ToUpper() + ": " + player.score;


             _textPrefab.transform.SetParent(scoreLayouts[j].transform, false);
         }

        GGJGameManager.SetMusic(true);

    }

    public void OnStateExit()
    {
        GGJGameManager.SetMusic(false);
    }

    public void OnStateUpdate(float deltaTime)
    {

    }
}
