using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject enemyPrefab;
    public float timeBetweenSpawns = 2f;
    public int enemiesPerWave = 5;
    public int totalWaves = 3;

    private Transform[] pathPoints;
    private Transform enemySpawnPoint;
    private int currentWave = 0;
    private int enemiesAlive = 0;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else
        { 
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        StartCoroutine(SetupAndStartGame());
    }

    IEnumerator SetupAndStartGame()
    {
        yield return new WaitUntil(() => ARPlacement.IsFieldPlaced);

        var field = ARPlacement.SpawnedField;

        if (field == null)
        {
            Debug.LogError("GameField not found in scene.");
            yield break;
        }

        Transform spawnPoint = field.transform.Find("EnemySpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogError("EnemySpawnPoint not found in GameField.");
            yield break;
        }
        enemySpawnPoint = spawnPoint;

        Transform pathGroup = field.transform.Find("PathGroup");
        if (pathGroup == null)
        {
            Debug.LogError("PathGroup not found in GameField.");
            yield break;
        }

        List<Transform> points = new()
        {
            spawnPoint
        };
        foreach (Transform child in pathGroup)
        {
            points.Add(child);
        }
        pathPoints = points.ToArray();

        StartCoroutine(SpawnWaves());
    }

    public void WinGame()
    {
        UIManager.instance.ShowWin();
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        UIManager.instance.ShowGameOver();
        Time.timeScale = 0f;
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            enemiesAlive = 0;
            UIManager.instance.UpdateWaveText(totalWaves, currentWave);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            if(currentWave < totalWaves)
            {
                yield return new WaitForSeconds(5f);
            }
        }

        yield return new WaitUntil(() => enemiesAlive <= 0);

        WinGame();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = enemySpawnPoint.position;
        if (pathPoints.Length > 0)
        {
            spawnPos.y = pathPoints[0].position.y;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        if (enemyObj.TryGetComponent<Enemy>(out var enemy))
        {
            enemiesAlive++;
            enemy.pathPoints = pathPoints;
            enemy.OnDeath += OnEnemyKilled;
        }
        else
        {
            Debug.LogWarning("Spawned enemy missing Enemy script!");
        }
    }

    void OnEnemyKilled()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}
