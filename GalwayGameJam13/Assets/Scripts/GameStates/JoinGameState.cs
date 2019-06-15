using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinGameState : IGameState
{
    private GameObject _playerPrefab;
    private GameObject _canvasPrefab;
    private GameObject TextPrefab;
    private GameObject _textPrefab;

    public JoinGameState()
    {
        _playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        _canvasPrefab = Resources.Load("Prefabs/Canvas") as GameObject;
        TextPrefab = Resources.Load("Prefabs/Continue") as GameObject;
    }
    
    public void HandleInput(string key)
    {
        Player player;
        if(GGJGameManager.TryGetPlayer(key, out player))
        {
            GGJGameManager.RemovePlayer(key);
        }
        else
        {
            GameObject playerGO = GameObject.Instantiate(_playerPrefab);
            player = playerGO.GetComponent<Player>();
            player.Setup(key);
            GGJGameManager.AddPlayer(key, player);
        }
    }

    public void OnStateEnter()
    {
        GameObject canvas = Object.Instantiate(_canvasPrefab);
        _textPrefab = Object.Instantiate(TextPrefab);
        _textPrefab.transform.SetParent(canvas.transform, false);
    }

    public void OnStateExit()
    {

    }
}
