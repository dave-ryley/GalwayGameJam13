using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : IGameState
{
    private GameObject TextPrefab;
    private GameObject _textPrefab;

    public GameOverState()
    {
        TextPrefab = Resources.Load("Prefabs/Space") as GameObject;
    }

    public void HandleKeyDown(string key)
    {
        if (key.Equals("space"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
      
        if (key.Equals("escape"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    public void HandleKeyHold(string key)
    {

    }

    public void HandleKeyUp(string key)
    {

    }

    public void OnStateEnter()
    {

        _textPrefab = Object.Instantiate(TextPrefab);
        _textPrefab.GetComponent<Text>().text = "Press Space to Reset" ;
        _textPrefab.transform.SetParent(Object.FindObjectOfType<Canvas>().transform, false);
        Time.timeScale = 0;
    }

    public void OnStateExit()
    {
        Time.timeScale = 1;
    }

    public void OnStateUpdate(float deltaTime)
    {

    }
}
