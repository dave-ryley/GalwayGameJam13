using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGameState : IGameState
{
    private Dictionary<string, Player> _players;
    private GameObject _playerPrefab;

    public JoinGameState()
    {
        _players = new Dictionary<string, Player>();
        _playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
    }

    public void HandleInput(string key)
    {
        Player player;
        if(_players.TryGetValue(key, out player))
        {
            _players.Remove(key);
            Object.Destroy(player.gameObject);
        }
        else
        {
            GameObject playerGO = GameObject.Instantiate(_playerPrefab);
            player = playerGO.GetComponent<Player>();
            _players.Add(key, player);
        }
    }

    public void OnStateEnter()
    {

    }

    public void OnStateExit()
    {

    }
}
