using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBossManager : BossManager
{
    [SerializeField] public EnemyWeapon[] enemyWeapons;
    [SerializeField] public EnemyWeapon equipAnim;
    public float attackFrequency = 15.0f;
    private int attackCounter = 0;
    public int currentAttackId = 0;

    public void PerformBossShoot()
    {
        enemyWeapons[attackCounter].PerformShoot();
        currentAttackId = attackCounter;
        if ((attackCounter + 1) < enemyWeapons.Length)
        {
            attackCounter++;
        }
        else
        {
            attackCounter = 0;
        }
    }

    public void AbortBossShoot()
    {
        enemyWeapons[currentAttackId].AbortShoot();
    }
}
