using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject ControlPanel, SinglePlayerControls, Co_OpControlPanel;
    private void Start()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(false);
    }

    public void EnableControlPanel()
    {
        Co_OpControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        ControlPanel.SetActive(true);
    }
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCo_opModeGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenSinglePlayerContols()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(true);
        Co_OpControlPanel.SetActive(false);
    }

    public void OpenCo_OpControls()
    {
        ControlPanel.SetActive(false);
        SinglePlayerControls.SetActive(false);
        Co_OpControlPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
