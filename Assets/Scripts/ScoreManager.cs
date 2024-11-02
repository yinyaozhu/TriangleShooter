using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int highscore;

    public UnityEvent OnScoreUpdated;
    public UnityEvent OnHighScoreUpdated;

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore");
        OnHighScoreUpdated?.Invoke();
        GameManager.GetInstance().OnGameStart += OnGameStart;
    }

    public void OnGameStart() { score = 0; }
    public int GetScore() { return score; }
    public int GetHighScore() {  return highscore; }
    public void InCrementScore() {
        score++;
        OnScoreUpdated?.Invoke();

        if (score > highscore) { 
            highscore = score;
            OnScoreUpdated?.Invoke();
        }

        if (score % 10 == 0) {
            //if we have 4 enemy, swarp every 10 scores
            GameManager.GetInstance().stageIndex = (GameManager.GetInstance().stageIndex + 1) % 4;
        }
        

    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
