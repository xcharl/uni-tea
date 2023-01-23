using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public int BricksCount { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }

    private void Awake()
    {
        Debug.Log("Scene: " + SceneManager.GetActiveScene().name);
        if (MainManager.Instance != null)
        {
            Debug.Log("Multiple main managers");
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        MainManager.Instance = this;

        this.Lives = 3;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        SetLivesGuiText();
        SetScoreGuiText();
    }

    public void RemoveLife()
    {
        this.Lives--;
        SetLivesGuiText();
        if (this.Lives == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void SetLivesGuiText()
    {
        var livesText = GameObject.Find("LivesText")?.GetComponent<Text>();
        if (livesText != null)
        {
            livesText.text = "Lives: " + this.Lives;
        }
    }

    public void AddScore(int score)
    {
        this.Score += score;
        SetScoreGuiText();
    }

    private void SetScoreGuiText()
    {
        var scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        if (scoreText != null)
        {
            scoreText.text = "Score: " + this.Score;
        }
    }

    public void ProgressIfWon()
    {
        if (this.BricksCount == 0)
        {
            Debug.Log("No bricks left");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
