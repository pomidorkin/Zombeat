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
    public Animator animator;
    public Vehicle vehicle;

    /*private bool stateMachineSetUp = false;*/

    // Needed for the DeathState
    [SerializeField] public Ragdoll ragdoll;
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] public Enemy enemyScript;

    // Targets
    [SerializeField] public Transform headPosition;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("WeaponBase").transform;
            vehicle = target.GetComponentInParent<Vehicle>();
        }

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.RegisterState(new AiStaggerState());
        stateMachine.RegisterState(new AiSpawnState());
        stateMachine.RegisterState(new AiCrawlerIdleState());
        stateMachine.RegisterState(new AiCrawlerChaseAttackState());
        stateMachine.RegisterState(new AiTeleportationState());
        stateMachine.RegisterState(new AiShootState());
        stateMachine.ChangeState(initialState);
        //stateMachineSetUp = true;
    }

    void Update()
    {
        /*if (stateMachineSetUp)
        {*/
            stateMachine.Update();
        //}
    }

    public void DealDamageToVehicle(float val)
    {
        vehicle.DealDamageToVehicle(val);
    }
}
