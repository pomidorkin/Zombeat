using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAOEAttack : EnemyWeapon
{
    // Position where the particle effect should be instantiated
    //public Vector3 spawnPosition;

    // Optional: Rotation for the particle effect
    public Quaternion spawnRotation = Quaternion.identity;

    // Sword prefab
    //public GameObject swordPrefab;

    // Number of swords to instantiate
    public int numberOfSwords = 5;

    // Height to spawn the swords above the particle effect
    public float spawnHeight = 10f;

    // Speed at which the swords move towards the particle effect
    public float swordSpeed = 5f;

    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] CrossProjectile projectilePrefab;
    public override void PerformShoot()
    {
        if (particleSystem != null)
        {
            // Instantiate the particle effect prefab
            GameObject particleEffect = Instantiate(particleSystem.gameObject, agent.vehicle.transform.position, spawnRotation);

            // Optionally: You can destroy the particle effect after a certain duration
            // This is useful if you want the effect to disappear after it finishes
            Destroy(particleEffect, 3f); // Adjust the duration based on your particle system's lifetime

            // Instantiate the swords and make them move towards the particle effect
            /*for (int i = 0; i < numberOfSwords; i++)
            {
                InstantiateSword();
            }*/
            StartCoroutine(SpawnObjectsWithDelay());
        }
        ReturnBackToChase();
    }

    private IEnumerator SpawnObjectsWithDelay()
    {
        for (int i = 0; i < numberOfSwords; i++)
        {
            // Instantiate the object at the specified spawn point
            InstantiateSword();

            // Wait for the specified delay before continuing
            yield return new WaitForSeconds(0.2f);
        }
    }

    void InstantiateSword()
    {
        if (projectilePrefab != null)
        {
            // Randomize the spawn position for the swords above the particle effect
            Vector3 randomPosition = agent.vehicle.transform.position + new Vector3(Random.Range(-10f, 10f), spawnHeight, Random.Range(-10f, 10f));
            GameObject projectile = Instantiate(projectilePrefab.gameObject, randomPosition, Quaternion.identity);
            projectile.GetComponent<CrossProjectile>().InitializeProjectile(agent.vehicle.transform.position);

            // Start moving the sword towards the particle effect
            //StartCoroutine(MoveSwordTowards(sword, spawnPosition));
        }
        else
        {
            Debug.LogError("Sword Prefab is not assigned.");
        }
    }
    public override void AbortShoot()
    {
    }
}
