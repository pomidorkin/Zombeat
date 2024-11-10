using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpawner : MonoBehaviour
{
    [SerializeField] private BomberGate[] bomberGates;
    [SerializeField] private GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField] private int totalEnemiesToSpawn = 30;
    [SerializeField] private float spawnInterval = 5f; // Time between enemy spawns
    [SerializeField] private float portalEffectDuration = 3f; // Time to play particle effect
    [SerializeField] private float enemySpawnDelay = 2f; // Duration to stop particle effect after spawning

    private int enemiesSpawned = 0;
    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning && enemiesSpawned < totalEnemiesToSpawn)
        {
            // Select a random BomberGate
            BomberGate selectedGate = bomberGates[Random.Range(0, bomberGates.Length)];

            
                // Play the portal particle effect
                selectedGate.portalParticleSystem.Play();

                // Wait for the duration of the portal effect
                yield return new WaitForSeconds(portalEffectDuration);

            // Check if the selected gate is occupied
            if (!selectedGate.IsOccupied())
            {
                // Spawn the enemy at the selected gate's spawn position
                Instantiate(enemyPrefab, selectedGate.spawnPosition.position, Quaternion.identity);
            }

            enemiesSpawned++;
            // Stop the particle effect after spawning
            selectedGate.portalParticleSystem.Stop();

            // Wait for the specified spawn interval before the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }

        // Check for win condition
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (enemiesSpawned >= totalEnemiesToSpawn)
        {
            Debug.Log("All enemies spawned! You win!");
            // Implement any additional win logic here
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        Debug.Log("Spawning stopped.");
    }
}
