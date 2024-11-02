using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverLbl;
    [SerializeField] private TMP_Text txtMenuHighScore;

    private Player player;
    private ScoreManager scoreManager;

    [Header("GamePlay")]
    [SerializeField] private TMP_Text txtHighScore;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHealth;

    [Header("SuperPower")]
    [SerializeField] private GameObject NukePlaceHolder;
    [SerializeField] private GameObject NukeTemplate;
    private Stack<GameObject> NukeIconTraceBack;

    [Header("SuperStatus")]
    [SerializeField] private GameObject PowerupStatusHolder;

    // Start is called before the first frame update
    void Awake()
    {
        scoreManager = GameManager.GetInstance().scoreManager;
        // instance actions
        GameManager.GetInstance().OnGameStart += GameStarted;
        GameManager.GetInstance().OnGameOver += GameOver;

        gameOverLbl.SetActive(false);

        NukeIconTraceBack = new Stack<GameObject>();
    }

    private void OnDisable()
    {
        
    }

    public void UpdateHealth(float currentHealth) { 
        txtHealth.SetText(Math.Round(currentHealth,2).ToString());
    }

    //used in OnScoreUpdated unity event
    public void UpdateScore()
    {
        txtScore.SetText(scoreManager.GetScore().ToString());
    }

    public void UpdateHighScore() { 
        txtHighScore.SetText(scoreManager.GetHighScore().ToString());
        txtMenuHighScore.SetText($"High Score: {scoreManager.GetHighScore()}");
    }

    public void GameStarted() { 
        player = GameManager.GetInstance().GetPlayer();
        player.health.OnHealthUpdate += UpdateHealth;

        player.superPower.SuperPowerUpdate += ChangeNuke;
        player.superStatus.SuperStatusUpdate += SwitchGunPower;

        menuPanel.SetActive(false);
    }

    private void ChangeNuke(bool aSwitch)
    {
        if (aSwitch)
        {
            int temp = NukeIconTraceBack.Count;

            // Instantiate the button dynamically
            GameObject newIcon = GameObject.Instantiate(NukeTemplate, new Vector2(NukePlaceHolder.transform.position.x + temp * 13, NukePlaceHolder.transform.position.y), NukePlaceHolder.transform.rotation);

            // Set the parent of the new button (In my case, the parent of tutorialButton)
            newIcon.transform.SetParent(NukePlaceHolder.transform);
            
            NukeIconTraceBack.Push(newIcon);
        }
        else
        {
            Destroy(NukeIconTraceBack.Pop());
        }
    }

    private void SwitchGunPower(bool aSwitch) {
        if (aSwitch)
        {
            PowerupStatusHolder.gameObject.SetActive(true);
            StartCoroutine(SuperStatusEnd());
        }
        else {
            PowerupStatusHolder.gameObject.SetActive(false);
        }
    }

    public IEnumerator SuperStatusEnd()
    {
        
        yield return new WaitForSeconds(3f);
        
        var player = GameManager.GetInstance().GetPlayer();
        player.superStatus.turnSuperStatusOff();
    }


    public void GameOver() {
        txtScore.SetText("0");//set to 0

        gameOverLbl.SetActive(true);
        menuPanel.SetActive(true);

        gameOverLbl.SetActive(true);

        player.superPower.SuperPowerUpdate -= ChangeNuke;
        player.superStatus.SuperStatusUpdate -= SwitchGunPower;
    }
}
