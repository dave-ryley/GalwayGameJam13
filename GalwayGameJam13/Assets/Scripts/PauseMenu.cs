using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;
    private GameObject _pausePanel;
    public Canvas Canvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _pausePanel == null)
        {
            Pause();
        }
    }
    void QuitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ResumeLevel()
    {
        Destroy(_pausePanel);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _pausePanel = Instantiate(PausePanel);
        _pausePanel.transform.Find("ResumeButton").GetComponent<Button>().onClick.AddListener(ResumeLevel);
        _pausePanel.transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(QuitLevel);
        _pausePanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
    }
}
