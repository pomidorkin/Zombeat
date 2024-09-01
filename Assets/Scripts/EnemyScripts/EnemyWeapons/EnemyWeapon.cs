using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected AiAgent agent;
    [SerializeField] protected GameObject weaponObject;
    [SerializeField] protected Transform raycastLocation;
    [SerializeField] protected ParticleSystem[] vfx;

    // Shooting details
    public float shootDistance = 100f; // Maximum distance of the raycast
    public LayerMask weaponBaseLayerMask; // Layer mask to identify obstacles
    public int damage = 10; // Damage dealt to the player
    public float durationOfSoot = 3.0f;

    public abstract void PerformShoot();
    public abstract void AbortShoot();

    protected void ReturnBackToChase()
    {
        agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
    }

    public void DropWeapon()
    {
        if (weaponObject != null)
        {
            weaponObject.GetComponent<BoxCollider>().enabled = true;
            weaponObject.AddComponent<Rigidbody>();
        }
    }

}