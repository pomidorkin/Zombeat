using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currecntHealth;
    [SerializeField] AiAgent agent;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    public float dieForce = 10f;
    public float blinkIntensity = 10;
    public float blinkDuration = 0.05f;
    float blinkTimer;

    // Stagger logic
    private float staggerCD = 5.0f;
    private float staggerTimer = 0.0f;

    // 7:50 https://www.youtube.com/watch?v=oLT4k-lrnwg&list=PLyBYG1JGBcd009lc1ZfX9ZN5oVUW7AFVy&index=3&ab_channel=TheKiwiCoder
    // Raycast hit example

    void Start()
    {
        currecntHealth = maxHealth;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            HitBox hitbox = rigidbody.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currecntHealth -= amount;
        if (currecntHealth <= 0)
        {
            Die(direction);
        }

        if (staggerTimer > staggerCD)
        {
            staggerTimer = 0.0f;
            if (agent.enemyScript.canBeStaggered)
            {
                agent.stateMachine.ChangeState(AiStateId.Stagger);
            }
        }

        //blinkTimer = blinkDuration;
    }

    /*private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
        staggerTimer += Time.deltaTime;
    }*/

    /*private void Update()
    {
        // Update blink timer
        blinkTimer -= Time.deltaTime;

        // Calculate the lerp value
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);

        // Determine the glow intensity based on the lerp value
        Color originalColor = skinnedMeshRenderer.material.color;
        Color emissionColor = originalColor * blinkIntensity * lerp;

        // Apply the emission color to the material
        skinnedMeshRenderer.material.SetColor("_EmissionColor", emissionColor);

        // Increase stagger timer
        staggerTimer += Time.deltaTime;
    }*/

    private void Die(Vector3 direction)
    {
        // Example of how the run-time properties can be sent to the states:
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
}
