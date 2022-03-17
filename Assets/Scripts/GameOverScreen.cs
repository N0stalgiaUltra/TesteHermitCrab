using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, highscoreText;
    [SerializeField] private Button menu, restart;

    void Start()
    {
        SetText();
        SetButtons();
    }
    
    private void SetText()
    {
        scoreText.text = $"Score: {GameManager.instance.Score}";
        highscoreText.text = $"HighScore: {GameManager.instance.HighScore}";
    }

    private void SetButtons()
    {
        menu.onClick.AddListener(() => GameManager.instance.Menu());
        restart.onClick.AddListener(() => GameManager.instance.Restart());
    }
}
