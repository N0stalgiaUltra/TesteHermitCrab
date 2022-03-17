using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int highScore;

    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Player playerRef;

    public static bool isGameStarted = false;

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

        if (startText != null)
            startText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(scoreText != null)
            scoreText.text = $"score: {score}";
        if(highScoreText != null)
            highScoreText.text = $"highscore: {highScore}";
    }
    public void GameStart() {
        startText.gameObject.SetActive(false);
        isGameStarted = true;
        playerRef.StartGame();
    }

    public void GameOver(bool win) {

        if (win)
            title.text = "YOU WIN!";
        else
            title.text = "GAME OVER";
        
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
        isGameStarted = false;
        SceneManager.LoadScene(1);

    }

    public void Menu()
    {
        isGameStarted = false;
        SceneManager.LoadScene(0);
    }
    public int Score { get => this.score; set => score = value; }
    public int HighScore { get => this.highScore; }
}
