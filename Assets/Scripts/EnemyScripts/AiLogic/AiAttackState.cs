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
        ResetAttackTimer(agent);
    }

    private static void Attack(AiAgent agent)
    {
        agent.animator.SetInteger("AttackIndex", Random.Range(0, agent.config.numberOfAttackAnims));
        agent.animator.SetTrigger("Attack");
        agent.enemyScript.attackCollider.SetActive(true);
    }

    public void Update(AiAgent agent)
    {
        changeStateTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            Attack(agent);
            ResetAttackTimer(agent);
            agent.animator.SetInteger("IdleIndex", Random.Range(0, agent.config.numberOfIdleAnims));
            agent.gameObject.transform.LookAt(new Vector3(agent.target.position.x, agent.gameObject.transform.position.y, agent.target.position.z)); // Looking at player when attacking
        }
        // Check if the target gor further away anf then transition back to chase state
        if (changeStateTimer < 0)
        {
            if (agent.enemyScript.enemyType != EnemyType.Boss)
            {
                if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) >= (agent.config.attackDistance + 1f))
                {
                    agent.animator.SetTrigger("Chase");
                    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
                }
            }
            else
            {
                if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) >= (agent.config.bossAttackDistance + 1f))
                {
                    agent.animator.SetTrigger("Chase");
                    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
                }
            }
            
            changeStateTimer = agent.config.changeStateTimer;
        }
    }

    private void ResetAttackTimer(AiAgent agent)
    {
        Debug.Log("ResetAttackTimer");
        if (agent.enemyScript.enemyType != EnemyType.Boss)
        {
            attackTimer = agent.config.attackFrequency;
        }
        else
        {
            attackTimer = agent.config.bossAttackFrequency;
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
