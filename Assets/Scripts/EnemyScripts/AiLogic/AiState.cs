using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    ChasePlayer,
    Death,
    Idle,
    Attack,
    Stagger,
    Spawn,
    CrawlerIdle,
    CrawlerChaseAttack,
    Teleportation,
    Shoot
}


public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
