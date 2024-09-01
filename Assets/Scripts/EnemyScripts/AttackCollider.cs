using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] AiAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            agent.DealDamageToVehicle(agent.enemyScript.baseAttackDamage);
            Debug.Log("Collision detected");
        }
        Debug.Log("Dealing damage to the vehicle");
        gameObject.SetActive(false);
    }
}