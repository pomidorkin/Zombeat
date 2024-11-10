using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponStaff : EnemyWeapon
{
    public override void AbortShoot()
    {
    }

    public override void PerformShoot()
    {
        //agent.animator.SetTrigger("Shoot");

        // Determine the direction to the player
        Vector3 direction = (agent.vehicle.enemyAimPoint.position - raycastLocation.position).normalized;

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

    private IEnumerator ReturnBackToChaseState()
    {
        yield return new WaitForSeconds(durationOfSoot);
        ReturnBackToChase();
    }
}