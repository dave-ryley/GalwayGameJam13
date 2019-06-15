using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GGJInputManager))]
public class GGJGameManager : MonoBehaviour
{
    private GGJInputManager _inputManager;
    private Dictionary<string, IGameState> _gameStates;
    private IGameState _curGameState;

    void Awake()
    {
        _inputManager = GetComponent<GGJInputManager>();
        _inputManager.Setup(this);
    }

#region Public members

    public void HandleInput(string inputKey)
    {
        _curGameState.HandleInput(inputKey);
    }

    public void SetState(string stateKey)
    {
        IGameState newState;
        if(_gameStates.TryGetValue(stateKey, out newState))
        {
            _curGameState.OnStateExit();
            _curGameState = newState;
            newState.OnStateEnter();
        }
    }

#endregion
}
