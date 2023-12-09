using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    [SerializeField] private Animator animator;

    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>(); // Another solution: Assign in inspector;
        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }

        animator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        Rigidbody rigidbody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
