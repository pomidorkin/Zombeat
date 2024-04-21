using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    private float changeStateTimer = 0.0f;
    private float attackTimer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }
    public void Enter(AiAgent agent)
    {
        Attack(agent);
        attackTimer = agent.config.attackFrequency;
    }

    private static void Attack(AiAgent agent)
    {
        agent.animator.SetInteger("AttackIndex", Random.Range(0, agent.config.numberOfAttackAnims));
        agent.animator.SetTrigger("Attack");
    }

    public void Update(AiAgent agent)
    {
        changeStateTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            Attack(agent);
            attackTimer = agent.config.attackFrequency;
            agent.animator.SetInteger("IdleIndex", Random.Range(0, agent.config.numberOfIdleAnims));
        }
        // Check if the target gor further away anf then transition back to chase state
        if (changeStateTimer < 0)
        {
            if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) >= (agent.config.attackDistance + 1f))
            {
                agent.animator.SetTrigger("Chase");
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }

            changeStateTimer = agent.config.changeStateTimer;
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
