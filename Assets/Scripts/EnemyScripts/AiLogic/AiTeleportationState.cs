using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTeleportationState : AiState
{
    private bool timerStarted = false;
    private bool teleported = false;
    private float timer = 0.0f;
    private Vector3 newPosition;
    private AiAgent agent;
    public AiStateId GetId()
    {
        return AiStateId.Teleportation;
    }
    public void Enter(AiAgent agent)
    {
        this.agent = agent;
        teleported = false;
        timerStarted = false;
        timer = 0.0f;
        agent.navMeshAgent.enabled = false;
        agent.enemyScript.InstantiateParticleEffect(agent.transform.position, new Vector3(agent.target.position.x, 0, agent.target.position.z));
        agent.animator.SetTrigger("Underground");
        //agent.enemyScript.SetMeshesVisible(false);
        timerStarted = true;
    }

    private void TeleportToThePlayer(AiAgent agent)
    {
        // Calculate direction from this object to the target object
        Vector3 directionToTarget = (agent.target.position - agent.transform.position).normalized;

        // Calculate the new position, offset by the desired distance
        newPosition = agent.target.position - directionToTarget * /*distanceFromTarget*/3.0f;
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.enabled = true;
    }

    private void GoBackToChase(AiAgent agent)
    {
        // Teleport this object to the calculated position
        
        agent.stateMachine.ChangeState(AiStateId.ChasePlayer);

    }

    public void Update(AiAgent agent)
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (!teleported && timer >= 1.5f)
            {
                TeleportToThePlayer(agent);
                agent.animator.SetTrigger("Appear");
               
                //agent.enemyScript.SetMeshesVisible(true);
                agent.transform.position = newPosition;
                agent.enemyScript.InstantiateAppearEffect();
                teleported = true;
                timerStarted = false;
                timer = 0.0f;
                GoBackToChase(agent);
            }
        }
    }
}
