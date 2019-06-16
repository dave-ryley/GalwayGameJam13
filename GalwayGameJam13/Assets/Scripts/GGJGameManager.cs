using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(GGJAudioManager))]
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

    private void handleKeyDown(string inputKey)
    {
        _curGameState.HandleKeyDown(inputKey);
    }

    private void handleKeyUp(string inputKey)
    {
        _curGameState.HandleKeyUp(inputKey);
    }

    private void handleKeyHold(string inputKey)
    {
        _curGameState.HandleKeyHold(inputKey);
    }

    private void setState(string stateKey)
    {
        IGameState newState;
        if (_gameStates.TryGetValue(stateKey, out newState))
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

    private void killPlayer(Player player)
    {
        _players.Remove(player.gameObject.name);
        Object.Destroy(player.gameObject);
        if (_players.Count <= 0 && _curGameState is PlayState )
        {
            setState("gameOver");
        }
    }

    private bool hasPlayer(string key)
    {
        return _players.ContainsKey(key);
    }

    private int playerCount()
    {
        return _players.Count;
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

    private GameObject[] sortTags(string tag)
    {
        GameObject[] foundObs = GameObject.FindGameObjectsWithTag(tag);
        System.Array.Sort(foundObs, CompareObNames);
        return foundObs;
    }


    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    #region Public members
    void Update()
    {
        _curGameState.OnStateUpdate(Time.deltaTime);
    }

    public static void AddPlayer(string playerKey, Player player)
    {
        _instance.addPlayer(playerKey, player);
    }

    public static bool HasPlayer(string key)
    {
        return _instance.hasPlayer(key);
    }

    public static GameObject[] SortTags(string tag)
    {
        return _instance.sortTags(tag);
    }
    public static bool TryGetPlayer(string key, out Player player)
    {
        return _instance._players.TryGetValue(key, out player);
    }

    public static List<string> GetPlayerNames()
    {
        return _instance.getPlayerNames();
    }

    public static void KillPlayer(Player player)
    {
        _instance.killPlayer(player);
    }

    public static int PlayerCount()
    {
        return _instance.playerCount();
    }
    public static void HandleKeyDown(string inputKey)
    {
        _instance.handleKeyDown(inputKey);
    }

    public static void HandleKeyHold(string inputKey)
    {
        _instance.handleKeyHold(inputKey);
    }

    public static void HandleKeyUp(string inputKey)
    {
        _instance.handleKeyUp(inputKey);
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
