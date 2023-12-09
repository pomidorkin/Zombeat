using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public Transform target;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;

    // Needed for the DeathState
    [SerializeField] public Ragdoll ragdoll;
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("TAG_HERE").transform;
        }

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();        
    }
}
