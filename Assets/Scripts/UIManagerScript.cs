using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText, EndScore, HowToStartText, HighScoreText;
    [SerializeField]
    private Sprite[] _Lives;
    [SerializeField]
    private Image Player1Lives_Img, Player2Lives_Img;
    [SerializeField]
    private bool IsPlayer1Dead = false, IsPlayer2Dead = false, IsActivateSpawn = true; 
    public bool IsAsteroid1Destoryed = false, IsAsteroid2Destroyed = false;
    private int HighScore, PlayerScore, CoOpHighScore;
    [SerializeField]
    private GameObject GameOverPanel, Pause_Panel;
    private SpawnManagerScript SpawnManager;
    private Animator PauseAnimator, GameOverAnimator;
    private GameManagementScript GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagementScript>();
        GameOverAnimator = GameObject.Find("Game_OverPanel").GetComponent<Animator>();
        PauseAnimator = GameObject.Find("PauseMenu_Panel").GetComponent<Animator>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();
        PauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        OnGameStart();
        if (PauseAnimator == null)
        {
            Debug.LogError("PauseAnimation is Null");
        }
        if(SpawnManager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }
        if (GameOverAnimator == null)
        {
            Debug.LogError("GameOVerAnimation is Null");
        }
        if (GameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
        if(GameManager.IsCo_OpMode == false)
        {
            HighScore = PlayerPrefs.GetInt("HighScore", 0);
            HighScoreText.text = "HIGH SCORE : " + HighScore;
        }
        else 
        {
            CoOpHighScore = PlayerPrefs.GetInt("CoOpHighScore", 0);
            HighScoreText.text = "HIGH SCORE : " + CoOpHighScore;
        }
    }

    private void OnGameStart()
    {
        HowToStartText.gameObject.SetActive(true);
        GameOverPanel.SetActive(false);
        Pause_Panel.SetActive(false);
        _ScoreText.text = "Score : " + 0;
    }
    // Update is called once per frame
    void Update()
    {
        PauseGame();
        EnableGameOver();
        if (GameManager.IsCo_OpMode == true)
        {
            if (IsActivateSpawn == true)
            {
                EnableEnemies();
            }
        }
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_Panel.SetActive(true);
            PauseAnimator.SetBool("IsGamePause", true);
            Time.timeScale = 0;
        }
    }

    public void Update_Score(int Points)
    {
        PlayerScore += Points;
        _ScoreText.text = "Score : " + PlayerScore;
    }

    public void GetHighScore()
    {
        if (GameManager.IsCo_OpMode == false)
        {
            if (PlayerScore > HighScore)
            {
                HighScore = PlayerScore;
                PlayerPrefs.SetInt("HighScore", HighScore);
                HighScoreText.text = "HIGH SCORE : " + HighScore;
            }
        }
        else 
        {
            if (PlayerScore > CoOpHighScore)
            {
                CoOpHighScore = PlayerScore;
                PlayerPrefs.SetInt("CoOpHighScore", CoOpHighScore);
                HighScoreText.text = "HIGH SCORE : " + CoOpHighScore; 
            }
        }
    }
    public void Update_Lives_Img(int CurrentLives)
    {
        Player1Lives_Img.sprite = _Lives[CurrentLives];
        if (CurrentLives == 0)
        {
            SpawnManager.OnPlayerDeadth();
            GameOverSequence();
            _ScoreText.gameObject.SetActive(false);
            EndScore.text = _ScoreText.text;
            GetHighScore();
        }
        
    }
    private void EnableEnemies()
    {
        if (IsAsteroid1Destoryed == true && IsAsteroid2Destroyed == true)
        {
            DisableHowToStartText();
            SpawnManager.StartSpawnManagers();
            SpawnManager.StartSpawnManagers();
            IsActivateSpawn = false;
        }
    }
    public void Update_Player1_Lives(int CurrentLives)
    {
        Player1Lives_Img.sprite = _Lives[CurrentLives];
        if (CurrentLives == 0)
        {
            IsPlayer1Dead = true;
        }
    }
    public void Update_Player2_Lives(int CurrentLives)
    {
        Player2Lives_Img.sprite = _Lives[CurrentLives];
        if (CurrentLives == 0)
        {
            IsPlayer2Dead = true;
        }
    }

    private void EnableGameOver()
    {
        if (IsPlayer1Dead == true && IsPlayer2Dead == true)
        {
            SpawnManager.OnPlayerDeadth();  
            GameOverSequence();
            _ScoreText.gameObject.SetActive(false);
            EndScore.text = _ScoreText.text;
            GetHighScore();
        }
    }
    public void DisableHowToStartText()
    {
        HowToStartText.gameObject.SetActive(false);
    }

    private void GameOverSequence()
    {   
        GameOverPanel.gameObject.SetActive(true);
        GameOverAnimator.SetBool("IsGameOver", true);
    }
}
