using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform[] enemySpawnPositions;
    public List<Enemy> activeEnemies;
    private int wave;
    [SerializeField] float spawnInterval = 0.2f;
    private float spawnIntervalCounter = 0.0f;
    [SerializeField] int maxAllowedNumberOfEnemies = 20;
    [SerializeField] float intervalBetweenWaves = 60.0f;
    private float intervalCounter = 0;

    [SerializeField] private OrchestraManager orchestraManager;


    private int spawningQuantity = 0;

    private void Start()
    {
        activeEnemies = new List<Enemy>();
        SpawnNewWave();
    }

    private void Update()
    {
        intervalCounter += Time.deltaTime;
        spawnIntervalCounter += Time.deltaTime;
        if (intervalCounter >= intervalBetweenWaves)
        {
            SpawnNewWave();
        }

        if (spawningQuantity > 0 && spawnIntervalCounter >= spawnInterval)
        {
            SpawnSingleEnemy();
            spawningQuantity--;
            spawnIntervalCounter = 0.0f;
        }
    }

    private void SpawnNewWave()
    {
        if (activeEnemies.Count < maxAllowedNumberOfEnemies)
        {
            intervalCounter = 0;
            for (int i = 0; i < (maxAllowedNumberOfEnemies - activeEnemies.Count); i++)
            {
                spawningQuantity++;
                if (activeEnemies.Count > maxAllowedNumberOfEnemies)
                {
                    break;
                }
            }
            wave++;
        }
    }

    private void SpawnSingleEnemy()
    {
        // Spawn different enemies depending on the current wave, now I am spawnin only 1 enemy type
        Enemy newEnemy = Instantiate(enemyPrefabs[0], enemySpawnPositions[Random.RandomRange(0, enemySpawnPositions.Length)].position, Quaternion.identity);
        newEnemy.enemySpawner = this;
        if (newEnemy.vocalEnemy)
        {
            newEnemy.enemySoundPlayer.orchestraManager = orchestraManager;
        }
        activeEnemies.Add(newEnemy);
    }

    public void RemoveMyselfFromList(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count <= 0) // or 5 may be
        {
            // Start new wave immediately
        }
    }
}
