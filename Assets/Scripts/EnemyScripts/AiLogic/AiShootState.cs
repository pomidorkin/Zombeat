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
        agent.enemyScript.enemyWeapon.PerformShoot();
    }
    public void Update(AiAgent agent)
    {
    }
    public void Exit(AiAgent agent)
    {
        agent.enemyScript.enemyWeapon.AbortShoot();
    }
}