using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStaggerState : AiState
{
    private float staggerTimer;
    public AiStateId GetId()
    {
        return AiStateId.Stagger;
    }
    public void Enter(AiAgent agent)
    {
        staggerTimer = agent.config.enemyStaggerTime;
        Debug.Log("Stagger state");
        agent.animator.SetInteger("StaggerIndex", Random.Range(0, agent.config.numberOfStaggerAnims));
        agent.animator.SetTrigger("Stagger");
        agent.navMeshAgent.isStopped = true;
    }

    public void Update(AiAgent agent)
    {
        staggerTimer -= Time.deltaTime;
        if (staggerTimer <= 0)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
