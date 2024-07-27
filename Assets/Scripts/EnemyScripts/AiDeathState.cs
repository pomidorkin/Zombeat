using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }
    public void Enter(AiAgent agent)
    {
        agent.enemyScript.RemoveSelf();
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1.0f;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        if (agent.enemyScript.enemyType != EnemyType.WallCrawler)
        {
            agent.enemyScript.enemySpawner.RemoveMyselfFromList(agent.enemyScript);
        }
        //skinnedMeshRenderer.updateWhenOffscreen = true;
    }
    public void Update(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }
}
