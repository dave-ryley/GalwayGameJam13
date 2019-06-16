using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinGameState : IGameState
{
    private const int BLOCK_START = -20;
    private const int BLOCK_COUNT = 40;
    private const int BLOCK_WIDTH = 1;
    private const float SPEED = 5f;

    
    private GameObject _playerPrefab;
    private GameObject _canvasPrefab;
    private GameObject TextPrefab;
    private GameObject _textPrefab;
    private GameObject _groundPrefab;
    private List<GameObject> _groundBlocks;

    public JoinGameState()
    {
        _groundBlocks = new List<GameObject>();
        _playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        _canvasPrefab = Resources.Load("Prefabs/Canvas") as GameObject;
        TextPrefab = Resources.Load("Prefabs/Continue") as GameObject;
        _groundPrefab = Resources.Load("Prefabs/GroundBlock") as GameObject;
    }

    public void HandleKeyDown(string key)
    {
        if(key.Equals("space"))
        {
            GGJGameManager.SetState("play");
        }
        else
        {
            Player player;
            if(GGJGameManager.TryGetPlayer(key, out player))
            {
                player.ResetSize();
                player.Jump();
            }
            else
            {
                GameObject playerGO = GameObject.Instantiate(_playerPrefab);
                player = playerGO.GetComponent<Player>();
                player.Setup(key);
                GGJGameManager.AddPlayer(key, player);
            }
        }
    }

    public void HandleKeyHold(string key)
    {
        Player player;
        if(GGJGameManager.TryGetPlayer(key, out player))
        {
            player.Grow();
        }
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
        GameObject canvas = Object.Instantiate(_canvasPrefab);
        for (int i = BLOCK_START; i < BLOCK_START + BLOCK_COUNT; i += BLOCK_WIDTH)
        {
            GameObject block = GameObject.Instantiate(_groundPrefab);
            block.transform.position = new Vector3(i, -3, 0);
            _groundBlocks.Add(block);
        }

    }

    public void OnStateExit()
    {
        GameObject.Destroy(GameObject.Find("Space"));

        for (int i = _groundBlocks.Count - 1; i >= 0; i--)
        {
            GameObject block = _groundBlocks[i];
            _groundBlocks.RemoveAt(i);
            Object.Destroy(block);
        }
    }

    public void OnStateUpdate(float deltaTime)
    {
        float offset = deltaTime * SPEED;
        for(int i = 0; i < _groundBlocks.Count;)
        {
            GameObject block = _groundBlocks[i];
            block.transform.position = new Vector3(block.transform.position.x - offset, -3, 0);
            if(block.transform.position.x < BLOCK_START)
            {
                block.transform.position = new Vector3(block.transform.position.x + BLOCK_COUNT, -3, 0);
            }
            else
            {
                i++;
            }
        }
    }
}
