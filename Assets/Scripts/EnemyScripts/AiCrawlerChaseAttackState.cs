using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCrawlerChaseAttackState : AiState
{
    // Improve tags
    private float timer = 0.0f;
    private float shootTimer = 0.0f;
    int playerLayerMask = LayerMask.GetMask("WeaponBase");

    public AiStateId GetId()
    {
        return AiStateId.CrawlerChaseAttack;
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
        shootTimer -= Time.deltaTime;
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

        if (shootTimer < 0)
        {
            RaycastShot(agent);
            shootTimer = agent.config.crawlerAttackSpeed;
        }
    }

    private void RaycastShot(AiAgent agent)
    {
        Ray ray = new Ray(agent.headPosition.position, agent.target.position);
        RaycastHit hit;

        // Check if the ray hits the cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayerMask))
        {

            GameObject player = hit.transform.gameObject;
            if (player.tag == "WeaponBase")
            {
                Debug.Log("Enemy was hit by ray");
                // Deal Damage,
                // Shoot Projectile
            }
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
