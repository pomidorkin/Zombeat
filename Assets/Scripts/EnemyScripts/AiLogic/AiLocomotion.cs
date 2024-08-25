using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    /*public Transform target;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    private float timer = 0.0f;*/

    NavMeshAgent agent;
    [SerializeField] Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        if (agent.hasPath)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude); // "Speed" is the foat name on the animator's blend tree
        }
        else
        {
            animator.SetFloat("Speed", 0); // "Speed" is the foat name on the animator's blend tree
        }
    }
        
}
