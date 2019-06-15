using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(GGJInputManager))]
public class GGJGameManager : MonoBehaviour
{
    private static GGJGameManager _instance;
    private GGJInputManager _inputManager;
    private Dictionary<string, IGameState> _gameStates;
    private IGameState _curGameState;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Assert.IsNull(_instance);
        _instance = this;
        _curGameState = new MainMenuState();
        _gameStates = new Dictionary<string, IGameState>()
        {
            { "mainMenu", _curGameState },
            { "joinGame", new JoinGameState() },
            { "play", new PlayState() },
            { "gameOver", new GameOverState() }
        };
        _inputManager = GetComponent<GGJInputManager>();
        _inputManager.Setup();

        _curGameState.OnStateEnter();
    }

    private void handleInput(string inputKey)
    {
        _curGameState.HandleInput(inputKey);
    }

    private void setState(string stateKey)
    {
        IGameState newState;
        if(_gameStates.TryGetValue(stateKey, out newState))
        {
            _curGameState.OnStateExit();
            _curGameState = newState;
            newState.OnStateEnter();
        }
    }

#region Public members

    public static void HandleInput(string inputKey)
    {
        _instance.handleInput(inputKey);
    }

    public static void SetState(string stateKey)
    {
        _instance.setState(stateKey);
    }

#endregion
}
