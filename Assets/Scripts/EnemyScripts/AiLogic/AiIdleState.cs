using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public bool confirmNoticedThePlayer = false;
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.animator.SetInteger("IdleIndex", Random.Range(0, 3)); // Put number of idle anims +1 here
        agent.animator.SetTrigger("Idle");
    }

    public void Update(AiAgent agent)
    {
        if (agent.enemyScript.enemyType != EnemyType.Boss)
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
                //agent.animator.SetTrigger("Chase");
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
        else
        {
            if (agent.enemyScript.noticedThePlayer && !confirmNoticedThePlayer)
            {
                confirmNoticedThePlayer = true;
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
    }
    public void Exit(AiAgent agent)
    {
    }
}
