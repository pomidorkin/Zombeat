using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyType enemyType;
    private AllEnemiesManager allEnemiesManager;
    public EnemySoundPlayer enemySoundPlayer;
    public EnemySpawner enemySpawner;
    [SerializeField] float bossAttackRange;
    public bool noticedThePlayer = false;

    private void OnEnable()
    {
        allEnemiesManager = FindFirstObjectByType<AllEnemiesManager>();
    }

    private void Start()
    {
        allEnemiesManager.AddMyselfToList(this);
        /*if (enemySpawner)
        {
            enemySpawner.activeEnemies.Add(this);
        }*/
    }

    public void RemoveSelf()
    {
        allEnemiesManager.RemoveMyselfFromList(this);
    }
}
