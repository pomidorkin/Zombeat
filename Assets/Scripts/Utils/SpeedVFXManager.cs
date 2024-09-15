using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedVFXManager : MonoBehaviour
{
    [SerializeField] MaterialPropertyController speedVFX;
    private RCC_CarControllerV3 carController;
    [SerializeField] VehicleWeaponInitializer vehicleWeaponInitializer;
    private Vehicle vehicle;

    private bool VFXIsPlaying = false;

    private void OnEnable()
    {
        vehicleWeaponInitializer.OnVehicleSpawned += VehicleSpawnedHandler;
    }

    private void OnDisable()
    {
        vehicleWeaponInitializer.OnVehicleSpawned -= VehicleSpawnedHandler;
    }

    private void VehicleSpawnedHandler()
    {
        if (vehicle == null)
        {
            vehicle = vehicleWeaponInitializer.vehicle;
            carController = vehicle.gameObject.GetComponent<RCC_CarControllerV3>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carController != null)
        {
            if (carController.speed > 70 && !VFXIsPlaying)
            {
                speedVFX.playingAllowed = true;
                VFXIsPlaying = true;
            }
            else if (carController.speed < 70 && VFXIsPlaying)
            {
                speedVFX.playingAllowed = false;
                VFXIsPlaying = false;
            }
        }
    }
}
