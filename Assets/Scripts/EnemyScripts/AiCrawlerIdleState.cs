using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCrawlerIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.CrawlerIdle;
    }
    public void Enter(AiAgent agent)
    {
    }

    public void Update(AiAgent agent)
    {
        // Player detecting logic
        Vector3 targetDirection = agent.target.position - agent.transform.position;
        if (targetDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        targetDirection.Normalize();

        float dotProduct = Vector3.Dot(targetDirection, agentDirection);

        if (dotProduct > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.CrawlerChaseAttack);
        }
    }
    public void Exit(AiAgent agent)
    {
    }
}
