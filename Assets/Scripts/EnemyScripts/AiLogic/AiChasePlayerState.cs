using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    private float timer = 0.0f;
    private float changeStateTimer = 0.0f;
    private float teleportationTimer = 0.0f;
    private float shootingTimer = 0.0f;
    private bool canShoot = false;

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        if (!agent.enemyScript.isDead)
        {
            agent.animator.SetTrigger("Chase");
            agent.navMeshAgent.isStopped = false;
            if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) <= agent.config.attackDistance)
            {
                agent.stateMachine.ChangeState(AiStateId.Attack);
            }
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
        /*if (agent.enemyScript.enemyWeapon != null)
        {
            canShoot = true;
            Debug.Log("Can shoot");
        }*/
    }

    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        changeStateTimer -= Time.deltaTime;
        teleportationTimer += Time.deltaTime;
        shootingTimer += Time.deltaTime;
        if (!agent.enemyScript.isBoss)
        {
            // Enemy Chase Logic (Not Boss)
            if (!agent.navMeshAgent.hasPath)
            {
                agent.navMeshAgent.destination = agent.target.position;
            }

            if (timer < 0)
            {
                // Check if the enemy should teleport
                if (teleportationTimer >= 15.0f && agent.enemyScript.canTeleport && Vector3.Distance(agent.target.position, agent.transform.position) > 20.0f) // TODO: teleportation cooldown and distance should be defined in the config file
                {
                    teleportationTimer = 0.0f;
                    agent.stateMachine.ChangeState(AiStateId.Teleportation);
                }
                else
                {
                    ContinueChase(agent);
                }
                timer = agent.config.maxTime;
            }
            // Check if the enemy should shoot, enemy shoots only when it has not attacked the player for shootingTimer >= 15.0f seconds! Maybe it has to be fixed
            if (agent.enemyScript.canShoot)
            {
                if (shootingTimer >= 15.0f && Vector3.Distance(agent.target.position, agent.transform.position) < 100.0f) // TODO: shooting cooldown and distance should be defined somewhere else
                {
                    shootingTimer = 0.0f;
                    agent.stateMachine.ChangeState(AiStateId.Shoot);
                }
                else
                {
                    ContinueChase(agent);
                }
                timer = agent.config.maxTime;
            }
            if (changeStateTimer < 0)
            {
                if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) <= agent.config.attackDistance)
                {
                    agent.stateMachine.ChangeState(AiStateId.Attack);
                }

                changeStateTimer = agent.config.changeStateTimer;
            }
        }
        else
        {
            // Boss Chase Logic
            if (!agent.navMeshAgent.hasPath)
            {
                agent.navMeshAgent.destination = agent.target.position;
            }
            if (agent.enemyScript.canShoot)
            {
                if (shootingTimer >= agent.enemyScript.humanoidBossManager.attackFrequency && Vector3.Distance(agent.target.position, agent.transform.position) < 100.0f) // TODO: shooting cooldown and distance should be defined somewhere else
                {
                    shootingTimer = 0.0f;
                    agent.stateMachine.ChangeState(AiStateId.Shoot);
                }
                else
                {
                    ContinueChase(agent);
                }
                timer = agent.config.maxTime;
            }
            if (changeStateTimer < 0)
            {
                if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) <= agent.config.attackDistance)
                {
                    agent.stateMachine.ChangeState(AiStateId.Attack);
                }

                changeStateTimer = agent.config.changeStateTimer; // How often Boss attacks
            }
        }
    }

    private static void ContinueChase(AiAgent agent)
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
    }

    public void Exit(AiAgent agent)
    {
    }
}
