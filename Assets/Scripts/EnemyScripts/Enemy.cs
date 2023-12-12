using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyType enemyType;
    private AllEnemiesManager allEnemiesManager;

    private void OnEnable()
    {
        allEnemiesManager = FindFirstObjectByType<AllEnemiesManager>();
    }

    private void Start()
    {
        allEnemiesManager.AddMyselfToList(this);
    }

    public void RemoveSelf()
    {
        allEnemiesManager.RemoveMyselfFromList(this);
    }
}
