using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Default,
    NormalZombie,
    WallCrawler
}

public class AllEnemiesManager : MonoBehaviour
{
    public delegate void UpdateListAction();
    public event UpdateListAction OnListUpdated;

    public List<Enemy> allEnemies;
    public List<Enemy> normalEnemies;
    public List<Enemy> wallCrawlers;

    public void AddMyselfToList(Enemy enemy)
    {
        allEnemies.Add(enemy);
        if (enemy.enemyType == EnemyType.NormalZombie)
        {
            normalEnemies.Add(enemy);
        }
        else if (enemy.enemyType == EnemyType.WallCrawler)
        {
            wallCrawlers.Add(enemy);
        }
        OnListUpdated();
    }

    public void RemoveMyselfFromList(Enemy enemy)
    {
        allEnemies.Remove(enemy);
        if (enemy.enemyType == EnemyType.NormalZombie)
        {
            normalEnemies.Remove(enemy);
        }
        else if (enemy.enemyType == EnemyType.WallCrawler)
        {
            wallCrawlers.Remove(enemy);
        }
        OnListUpdated();
    }

    public List<Enemy> GetInterestedEnemies(EnemyType interestedEnemies)
    {
        if (interestedEnemies == EnemyType.NormalZombie)
        {
            return normalEnemies;
        }
        else if (interestedEnemies == EnemyType.WallCrawler)
        {
            return wallCrawlers;
        }
        else
        {
            return allEnemies;
        }
    }
}
