using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 10f;
    public float maxSightDistance = 5.0f;
    public float crawlerAttackSpeed = 5.0f;
    public float changeStateTimer = 0.2f;
    public float attackDistance = 2.5f;
    public float attackFrequency = 3.0f;
    public float enemyStaggerTime = 2.0f;
    public float walkAwayDist = 2.5f;

    // Anims
    public int numberOfAttackAnims = 2;
    public int numberOfIdleAnims = 3;
    public int numberOfStaggerAnims = 1;

    // Boss
    public float bossAttackFrequency = 5.0f;
    public float bossAttackDistance = 10f;
}
