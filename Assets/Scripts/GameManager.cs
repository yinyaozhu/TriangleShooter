
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Experimental.GraphView;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Game Entities")]
    [SerializeField] public GameObject[] enemyPrefeb;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate; // ## DEBUG -> set to every low for debugging
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] public int stageIndex;// 0 refer to enemy index 0

    public Action OnGameStart;
    public Action OnGameOver;

    private GameObject tempEnemy;
    private bool isEnemySpawning;
    //private Queue<GameObject> enemyTraceback;
    private bool isPlaying;

    private Player player;
    public ScoreManager scoreManager;
    public PickupSpawner pickupSpawner;

    private static GameManager instance;

    public static GameManager GetInstance() { return instance; }

    void SetSingleton() {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    private void Awake()
    {
        SetSingleton();
    }

    //--------------Singleton pattern-------------------------

    private void Start()
    {
        FindPlayer();
    }

    // we call this from button
    public void StartGame() {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        player.OnDeath += StopGame;
        isPlaying = true;

        stageIndex = 0;
        
        OnGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter() {
        yield return new WaitForSeconds(2f);
        isEnemySpawning = true;
        StartCoroutine (EnemySpawner());
    }

    IEnumerator EnemySpawner()
    { 
        while (isEnemySpawning) {
            // Anthing abover here will fire when the Start Corountine is callled
            yield return new WaitForSeconds(1.0f / enemySpawnRate);
            // Anthing after the wait, will be called.
            CreateEnemy();
        }
    }

    void CreateEnemy() { 
        tempEnemy = Instantiate(enemyPrefeb[stageIndex]);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;

        switch (stageIndex) {
            case 0:
                tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(10f, 0.25f);
                break;
            case 1:
                tempEnemy.GetComponent<GunEnemy>().SetGunEnemy(60f, 0.75f);
                break;
            case 2:
                tempEnemy.GetComponent<ExpolderEnemy>().SetExpolderEnemy(2f, 0.1f);
                break;
            case 3:
                tempEnemy.GetComponent<ShooterEnemy>().SetShooterEnemy(50f, 2f);
                break;
        }
    }

    public void StopGame()
    {
        isEnemySpawning =false;
        scoreManager.SetHighScore();
        
        StartCoroutine(GameStopper());
    }

    IEnumerator GameStopper() {
        isEnemySpawning = false;
        isPlaying = false;
        yield return new WaitForSeconds(2f);
        

        foreach (Enemy item in FindObjectsOfType(typeof(Enemy))) {
            Destroy(item.gameObject);
        }

        foreach (Pickup item in FindObjectsOfType(typeof(Pickup)))
        {
            Destroy(item.gameObject);
        }

        foreach (Bullet item in FindObjectsOfType(typeof(Bullet)))
        {
            Destroy(item.gameObject);
        }

        OnGameOver?.Invoke();
    }

    public void NotifyDeath(Enemy enemy) {

        pickupSpawner.SpawnPickup(enemy.transform.position);

    }

    public void FindPlayer()
    {
        try
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        catch(NullReferenceException e) {
            Debug.Log("There's no player in scene.");
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

}
