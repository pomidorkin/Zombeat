using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_WEAPON_EQUIO : EnemyWeapon
{
    // From video: https://www.youtube.com/watch?v=_c-r1S4PZb4&list=PLyBYG1JGBcd009lc1ZfX9ZN5oVUW7AFVy&index=4
    [SerializeField] MeshSockets meshSockets;
    public override void PerformShoot()
    {
        agent.animator.SetTrigger("Equip");
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "equipWeapon")
        {
            meshSockets.Attach(weaponObject.transform, MeshSockets.SocketId.RightHand);
        }
    }

    public override void AbortShoot()
    {
    }
}
