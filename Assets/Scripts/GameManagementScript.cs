using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour
{
    public bool IsCo_OpMode = false;
    [SerializeField]
    private GameObject ControlPanel, SinglePlayerControls, Co_OpControlPanel, PausePanel;

    void Start()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(false);
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenControlPanel()
    {
        ControlPanel.SetActive(true);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void OpenSinglePlayerControls()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(true);
        Co_OpControlPanel.SetActive(false);
    }

    public void OpenCo_OpPlayerControls()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(true);
    }
    public void RestartCo_opModeGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
