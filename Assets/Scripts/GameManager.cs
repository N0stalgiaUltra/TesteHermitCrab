using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int highScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Player playerRef;

    public static bool isGameStarted;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("highscore");
        
        if(gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if(scoreText != null)
            scoreText.text = $"score: {score}";
        if(highScoreText != null)
            highScoreText.text = $"highscore: {highScore}";
    }
    public void GameStart() {
        isGameStarted = true;
        playerRef.StartGame();
    }

    public void GameOver() {
        
        isGameStarted = false;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
        }
        gameOverScreen.SetActive(true);
        //abrir tela de gameover
    }
    
    public void Restart() {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public int Score { get => this.score; set => score = value; }
    public int HighScore { get => this.highScore; }
}
