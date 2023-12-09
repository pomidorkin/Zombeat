using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    private float timer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
    }

    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.target.position;
        }

        if (timer < 0)
        {
            Vector3 direction = (agent.target.position - agent.navMeshAgent.destination);
            direction.y = 0;

            if (direction.sqrMagnitude > (agent.config.maxDistance * agent.config.maxDistance))
            {
                if (agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.target.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
