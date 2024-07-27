using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawnState : AiState
{
    float counter = 0.0f;
    float spawnCoolDown = 2.0f;
    float walkRadius = 15.0f;
    private float changeStateTimer = 0.0f;

    private bool destInitialized = false;
    private bool setSet = false;
    public AiStateId GetId()
    {
        return AiStateId.Spawn;
    }
    public void Enter(AiAgent agent)
    {
        changeStateTimer = agent.config.changeStateTimer;
    }
    public void Update(AiAgent agent)
    {
        if (!destInitialized)
        {
            counter += Time.deltaTime;
            if (counter >= spawnCoolDown)
            {
                WalkAway(agent);
                destInitialized = true;
            }
        }

        changeStateTimer -= Time.deltaTime;
        if (changeStateTimer < 0 && setSet)
        {
            Debug.Log("Dist less than attack dist: " + (Vector3.Distance(agent.gameObject.transform.position, agent.navMeshAgent.destination) <= agent.config.walkAwayDist));
            if (Vector3.Distance(agent.gameObject.transform.position, agent.navMeshAgent.destination) <= agent.config.walkAwayDist)
            {
                agent.stateMachine.ChangeState(AiStateId.Idle);
            }

            changeStateTimer = agent.config.changeStateTimer;
        }
    }

    private void WalkAway(AiAgent agent)
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += agent.transform.position;
        agent.navMeshAgent.destination = new Vector3(randomDirection.x, 0, randomDirection.z);
        Debug.Log("Distance between me and my destination: " + Vector3.Distance(agent.gameObject.transform.position, agent.navMeshAgent.destination));
        agent.animator.SetTrigger("Chase");
        setSet = true;
    }
    public void Exit(AiAgent agent)
    {

    }
}
