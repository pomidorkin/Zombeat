using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemyWeapon : EnemyWeapon
{
    private float timer = 0.0f;
    private bool activated = false;
    [SerializeField] float explosionTriggerDistance = 3.0f;
    [SerializeField] GameObject particleFXParent;

    /* Подобрать красивые анимации
     * Сделать звуковые эффекты
     */
    public override void AbortShoot()
    {
    }

    public override void PerformShoot()
    {
        agent.navMeshAgent.isStopped = false;
        ContinueChase(agent);
        activated = true;
        Debug.Log("Entered Kamikaze Weapon Script");
    }

    private void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                timer = 0.0f;
                if (Vector3.Distance(agent.gameObject.transform.position, agent.target.position) <= explosionTriggerDistance)
                {
                    Debug.Log("Explode!");
                    particleFXParent.SetActive(true);
                    particleFXParent.transform.SetParent(null);
                    agent.enemyScript.RemoveSelf();
                    Destroy(gameObject);
                }
                else
                {
                    ContinueChase(agent);
                }
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
}
