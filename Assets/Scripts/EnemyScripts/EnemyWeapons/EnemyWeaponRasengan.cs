using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponRasengan : EnemyWeapon
{
    private bool chaseActivated = false;
    private float rasenganTimer = 0.0f;
    private float chaseDuration = 5.0f;
    private float timer = 0.0f;

    Vector3 fireSpitDirection;
    private bool spitActivated = false;

    private bool canShoot = true;
    public override void PerformShoot()
    {
        /* //agent.animator.SetTrigger("Shoot");

         // Determine the direction to the player
         Vector3 direction = (agent.vehicle.enemyAimPoint.position - raycastLocation.position).normalized;

         // Cast a ray from the enemy to the player
         RaycastHit hit;
         if (Physics.Raycast(raycastLocation.position, direction, out hit, shootDistance, ~weaponBaseLayerMask))
         {
             // Check if the raycast hit the player
             if (hit.transform.tag == "Vehicle")
             {
                 // Apply damage to the player
                 Debug.Log("Player hit!");
             }
             else
             {
                 Debug.Log("Raycast hit an obstacle or another object!");
             }
         }

         //Debug.Log("raycastLocation.position: " + raycastLocation.position + ", direction")

         StartCoroutine(ReturnBackToChaseState());*/

        chaseActivated = true;
        agent.navMeshAgent.isStopped = false;
        agent.animator.SetTrigger("RasenganRun");
        vfx[0].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (canShoot)
        {
            if (chaseActivated)
            {
                rasenganTimer += Time.deltaTime;
                timer -= Time.deltaTime;
                if (rasenganTimer >= chaseDuration || Vector3.Distance(agent.gameObject.transform.position, agent.target.position) <= agent.config.attackDistance)
                {
                    rasenganTimer = 0.0f;
                    chaseActivated = false;
                    agent.navMeshAgent.isStopped = true;
                    ShootLaser();
                }
                if (timer < 0)
                {
                    ContinueChase(agent);
                    timer = agent.config.maxTime;
                }
            }
            if (spitActivated)
            {
                vfx[1].gameObject.transform.LookAt(fireSpitDirection);
            }
        }
    }

    private void ShootLaser()
    {
        agent.animator.SetTrigger("FireSpit");
        agent.navMeshAgent.isStopped = true;
        vfx[0].gameObject.SetActive(false);
        

        // Determine the direction to the player
        Vector3 direction = (agent.vehicle.enemyAimPoint.position - raycastLocation.position).normalized;
        fireSpitDirection = agent.vehicle.enemyAimPoint.position;
        spitActivated = true;

        // Cast a ray from the enemy to the player
        RaycastHit hit;
        if (Physics.Raycast(raycastLocation.position, direction, out hit, attackRange, ~weaponBaseLayerMask))
        {
            // Check if the raycast hit the player
            if (hit.transform.tag == "Vehicle")
            {
                // Apply damage to the player
                Debug.Log("Player hit!");
            }
            else
            {
                Debug.Log("Raycast hit an obstacle or another object!");
            }
        }

        //Debug.Log("raycastLocation.position: " + raycastLocation.position + ", direction")

        StartCoroutine(ReturnBackToChaseState());
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

    private IEnumerator ReturnBackToChaseState()
    {
        yield return new WaitForSeconds(durationOfSoot);
        //vfx[1].gameObject.SetActive(false);
        ReturnBackToChase();
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "enableLaser")
        {
            vfx[1].gameObject.SetActive(true);
        }
        else if (eventName == "disableLaser")
        {
            spitActivated = false;
            vfx[1].gameObject.SetActive(false);
        }
    }

    public override void AbortShoot()
    {
        canShoot = false;
        vfx[0].gameObject.SetActive(false);
        vfx[1].gameObject.SetActive(false);
    }
}
