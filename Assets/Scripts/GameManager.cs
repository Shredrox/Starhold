using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] pathPoints;
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    public float timeBetweenSpawns = 2f;
    public int enemiesPerWave = 5;
    public int totalWaves = 3;
    private int currentWave = 0;

    private void Start()
    {
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
            yield return new WaitForSeconds(5f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
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
