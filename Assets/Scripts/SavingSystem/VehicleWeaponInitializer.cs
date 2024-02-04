using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWeaponInitializer : MonoBehaviour
{
    [SerializeField] VehicleContainer vehicleContainer;
    [SerializeField] WeaponContainer weaponContainer;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] public OrchestraManager orchestraManager;

    private WeaponPlacer instantiatedWeaponPlacer;
    public Vehicle vehicle;

    //SoundUnitKey currentVehicleKey;
    //int currentVehicleBPM;

    public int selectedVehicleId; // Currently selected id

    private void Start()
    {
        selectedVehicleId = Progress.Instance.playerInfo.selectedVehicleId;
        GameObject instantiatedVehicle = Instantiate(vehicleContainer.vehiclePrefabs[selectedVehicleId], new Vector3(0, 0, 0), Quaternion.identity);
        instantiatedWeaponPlacer = instantiatedVehicle.GetComponentInChildren<WeaponPlacer>();
        vehicle = instantiatedWeaponPlacer.vehicle;
        instantiatedWeaponPlacer.vehicleWeaponInitializer = this;
        instantiatedWeaponPlacer.weaponContainer = this.weaponContainer;
        instantiatedWeaponPlacer.mainCamera = Camera.main;
        weaponManager.SpawnWeaponsOnVehicle(vehicle, selectedVehicleId);


        vehicle.SetVehicleSaveData(Progress.Instance.playerInfo.vehicleSaveDatas[selectedVehicleId]);
        orchestraManager.SetVehicle(vehicle);

    }
}
