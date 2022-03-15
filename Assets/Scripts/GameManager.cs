using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    private int highScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private Player playerRef;


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

    }

    private void Update()
    {
        scoreText.text = $"score: {score}";
        highScoreText.text = $"highscore: {highScore}";
    }
    public void GameStart() {
        playerRef.StartGame();
    }

    public void GameOver() {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
        }

        //abrir tela de gameover
    }
    
    public void Restart() { 
    
    }
}
