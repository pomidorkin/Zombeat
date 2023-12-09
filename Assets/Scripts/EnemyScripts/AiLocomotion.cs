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
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        /*if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!agent.hasPath)
        {
            agent.destination = target.position;
        }

        if (timer < 0)
        {
            Vector3 direction = (target.position - agent.destination);
            direction.y = 0;

            if (direction.sqrMagnitude > (maxDistance * maxDistance))
            {
                if (agent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.destination = target.position;
                }
            }
            timer = maxTime;
        }*/

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
