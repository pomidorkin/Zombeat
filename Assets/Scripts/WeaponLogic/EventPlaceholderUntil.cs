using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlaceholderUntil : MonoBehaviour
{

    [SerializeField] AllEnemiesManager allEnemiesManager;
    [SerializeField] OrchestraManager orchestraManager;
    private void Awake()
    {
        if (!allEnemiesManager)
        {
            allEnemiesManager = FindFirstObjectByType<AllEnemiesManager>();
        }
        allEnemiesManager.OnListUpdated += OnListUpdatedHandler;
    }
    private void OnEnable()
    {
        if (orchestraManager == null)
        {
            orchestraManager = FindFirstObjectByType<OrchestraManager>();
        }
        orchestraManager.OnVehicleSet += KeySpecifiedChecker;


    }

    private void KeySpecifiedChecker()
    {
        // This script is a placeholder.
        // It exists only because if a car has o weapons,
        // orchestraManager's event throws an error
    }

    private void OnDisable()
    {
        allEnemiesManager.OnListUpdated -= OnListUpdatedHandler;
    }

    private void OnListUpdatedHandler()
    {
        // This script is a placeholder.
        // It exists only because if a car has o weapons,
        // allEnemiesManager's event throws an error
    }
}
