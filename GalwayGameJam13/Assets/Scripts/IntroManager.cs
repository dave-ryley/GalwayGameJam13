using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public void Quit()
    {
        //Application.Quit();
        GGJGameManager.SetState("mainMenu");
    }

    public void startGame()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        GGJGameManager.SetState("joinGame");
    }
}
