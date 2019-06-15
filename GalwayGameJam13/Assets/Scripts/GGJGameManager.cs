using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(GGJInputManager))]
public class GGJGameManager : MonoBehaviour
{
    private static GGJGameManager _instance;
    private GGJInputManager _inputManager;
    private GGJAudioManager _audioManager;
    private Dictionary<string, IGameState> _gameStates;
    private IGameState _curGameState;
    private CameraGroupManager _cameraGroupManager;
    private Dictionary<string, Player> _players;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Assert.IsNull(_instance);
        _instance = this;
        _curGameState = new MainMenuState();
        _players = new Dictionary<string, Player>();
        _gameStates = new Dictionary<string, IGameState>()
        {
            { "mainMenu", _curGameState },
            { "joinGame", new JoinGameState() },
            { "play", new PlayState() },
            { "gameOver", new GameOverState() }
        };
        _inputManager = GetComponent<GGJInputManager>();
        _inputManager.Setup();
        _audioManager = GetComponent<GGJAudioManager>();
        _audioManager.Setup();

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

    private void setMusic(bool musicActive)
    {
        if (musicActive)
        {
            _audioManager.Stop();
            _audioManager.Play();
        }
        else { _audioManager.Stop(); }
    }

    private void registerCameraGroup(CameraGroupManager cameraGroupManager)
    {
        _cameraGroupManager = cameraGroupManager;
    }

    private void addPlayer(string playerKey, Player player)
    {
        _players.Add(playerKey, player);
        _cameraGroupManager.AddTarget(player.transform);
    }

    private bool hasPlayer(string key)
    {
        return _players.ContainsKey(key);
    }

    private List<string> getPlayerNames()
    {
        var names = new List<string>();
        foreach (var item in _players)
        {
            names.Add(item.Key);
        }
        return names;
    }


    #region Public members

    public static void AddPlayer(string playerKey, Player player)
    {
        _instance.addPlayer(playerKey, player);
    }

    public static bool HasPlayer(string key)
    {
        return _instance.hasPlayer(key);
    }

    public static bool TryGetPlayer(string key, out Player player)
    {
        return _instance._players.TryGetValue(key, out player);
    }

    public static List<string> GetPlayerNames()
    {
        return _instance.getPlayerNames();
    }

    public static void RemovePlayer(string key)
    {
        Player player = _instance._players[key];
        _instance._players.Remove(key);
        Object.Destroy(player.gameObject);
    }

    public static void HandleInput(string inputKey)
    {
        _instance.handleInput(inputKey);
    }

    public static void SetState(string stateKey)
    {
        _instance.setState(stateKey);
    }

    public static void SetMusic(bool musicActive)
    {
        _instance.setMusic(musicActive);
    }
    public static void RegisterCameraGroup(CameraGroupManager cameraGroupManager)
    {
        _instance.registerCameraGroup(cameraGroupManager);
    }

#endregion
}
