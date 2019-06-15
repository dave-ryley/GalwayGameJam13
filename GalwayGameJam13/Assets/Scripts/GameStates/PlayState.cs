using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private const float SPEED = 10f;
    private GameObject TextPrefab;
    private GameObject _textPrefab;
    private GameObject _levelPrefab;
    private Transform _level;

    public PlayState()
    {
        TextPrefab = Resources.Load("Prefabs/ScoreUI") as GameObject;
        _levelPrefab = Resources.Load("Prefabs/Level") as GameObject;
    }

    public void HandleKeyDown(string key)
    {
        Player player;
        if(GGJGameManager.TryGetPlayer(key, out player))
        {
            player.Jump();
        }
    }

    public void HandleKeyHold(string key)
    {
    }

    public void HandleKeyUp(string key)
    {
        Player player;
        if(GGJGameManager.TryGetPlayer(key, out player))
        {
            player.ResetSize();
        }
    }

    public void OnStateEnter()
    {
        // var scoreLayouts = GameObject.FindGameObjectsWithTag("Score");
        // List<string> names = GGJGameManager.GetPlayerNames();
        // int j = 0;
        // for (int i = 0; i < names.Count; i++)
        // {
        //     if (scoreLayouts[j].transform.childCount > 9) { j++; }
        //     _textPrefab = Object.Instantiate(TextPrefab);
        //     Player player;
        //     GGJGameManager.TryGetPlayer(names[i], out player);
        //     _textPrefab.GetComponent<Text>().text =  names[i].ToUpper() + ": " + player.score;


        //     _textPrefab.transform.SetParent(scoreLayouts[j].transform, false);
        // }

        _level = (Object.Instantiate(_levelPrefab) as GameObject).transform;
        _level.position = Vector3.zero;

        GGJGameManager.SetMusic(true);

    }

    public void OnStateExit()
    {
        GGJGameManager.SetMusic(false);
    }

    public void OnStateUpdate(float deltaTime)
    {
        float offset = deltaTime * SPEED;
        _level.position = new Vector3(_level.position.x - offset, 0f, 0f);
    }
}
