using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] pathPoints;
    public float timeBetweenWaves = 5f;
    public int enemiesPerWave = 5;

    private int waveNumber = 0;
    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            waveNumber++;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, pathPoints[0].position, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetPath(pathPoints);
    }
}
