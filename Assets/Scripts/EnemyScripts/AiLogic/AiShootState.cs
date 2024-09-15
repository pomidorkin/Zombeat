using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShootState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Shoot;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        if (!agent.enemyScript.isBoss)
        {
            agent.enemyScript.enemyWeapon.PerformShoot();
        }
        else
        {
            agent.enemyScript.humanoidBossManager.PerformBossShoot();
        }
    }
    public void Update(AiAgent agent)
    {
    }
    public void Exit(AiAgent agent)
    {
        if (!agent.enemyScript.isBoss)
        {
            agent.enemyScript.enemyWeapon.AbortShoot();
        }
        else
        {
            agent.enemyScript.humanoidBossManager.AbortBossShoot();
        }
    }
}