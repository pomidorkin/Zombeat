using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : MonoBehaviour
{
    private Animator animator;
    [SerializeField] AiAgent agent;

    void Start()
    {
        animator = agent.animator;
    }

    public void PlayRandomAnim()
    {
        animator.SetInteger("IdleIndex", Random.Range(0, agent.config.numberOfIdleAnims));
    }
}
