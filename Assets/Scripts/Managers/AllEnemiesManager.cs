using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyType
{
    Default,
    NormalZombie,
    WallCrawler,
    Boss,
    Kamikaze
}

public class AllEnemiesManager : MonoBehaviour
{
    public delegate void UpdateListAction();
    public event UpdateListAction OnListUpdated;

    public List<Enemy> allEnemies;
    public List<Enemy> normalEnemies;
    public List<Enemy> wallCrawlers;

    public bool eventsAllowed = false;

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
        if (eventsAllowed)
        {
            OnListUpdated();
        }
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
        if (eventsAllowed)
        {
            OnListUpdated();
        }
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

    public void TriggerEvent()
    {
        if (eventsAllowed)
        {
            OnListUpdated();
        }
    }
}
