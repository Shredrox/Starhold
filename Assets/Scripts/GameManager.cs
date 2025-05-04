using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeBetweenSpawns = 2f;
    public int enemiesPerWave = 5;
    public int totalWaves = 3;

    private Transform[] pathPoints;
    private Transform enemySpawnPoint;
    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SetupAndStartGame());
    }

    IEnumerator SetupAndStartGame()
    {
        yield return new WaitUntil(() => ARPlacement.IsFieldPlaced);

        //GameObject field = GameObject.FindWithTag("GameField");
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

        // Get path points
        Transform pathGroup = field.transform.Find("PathGroup");
        if (pathGroup == null)
        {
            Debug.LogError("PathGroup not found in GameField.");
            yield break;
        }

        //int count = pathGroup.childCount;
        //pathPoints = new Transform[count];
        //for (int i = 0; i < count; i++)
        //{
        //    pathPoints[i] = pathGroup.GetChild(i);
        //}

        List<Transform> points = new();
        foreach (Transform child in pathGroup)
        {
            points.Add(child);
        }
        pathPoints = points.ToArray();

        // Start the wave loop
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log("Wave " + currentWave);
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return new WaitForSeconds(5f); // Time between waves
        }
    }

    //void SpawnEnemy()
    //{
    //    GameObject enemyObj = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
    //    Enemy enemy = enemyObj.GetComponent<Enemy>();

    //    if (enemy != null)
    //    {
    //        enemy.pathPoints = pathPoints;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Spawned enemy missing Enemy script!");
    //    }
    //}

    void SpawnEnemy()
    {
        Vector3 spawnPos = enemySpawnPoint.position;
        if (pathPoints.Length > 0)
        {
            spawnPos.y = pathPoints[0].position.y;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.pathPoints = pathPoints;
        }
        else
        {
            Debug.LogWarning("Spawned enemy missing Enemy script!");
        }
    }

    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length == 0)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            if (pathPoints[i] != null && pathPoints[i + 1] != null)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
            }
        }
    }
}
