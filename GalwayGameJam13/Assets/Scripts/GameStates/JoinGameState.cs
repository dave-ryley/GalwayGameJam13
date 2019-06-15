using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGameState : IGameState
{
    private GameObject _playerPrefab;

    public JoinGameState()
    {
        _playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
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

    }

    public void OnStateExit()
    {

    }
}
