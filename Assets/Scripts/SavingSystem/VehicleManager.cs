using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] VehicleWeaponInitializer vehicleWeaponInitializer;
    //[SerializeField] WeaponSelectionUnit selectionButton;
    [SerializeField] GameObject parentHolder;

    public int selectedVehicleId = 0;

    public void SpawnVehice(int id)
    {
        vehicleWeaponInitializer.SelectVehicle(id);
    }
}
