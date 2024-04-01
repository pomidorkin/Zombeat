using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWeaponInitializer : MonoBehaviour
{
    [SerializeField] VehicleContainer vehicleContainer;
    [SerializeField] WeaponContainer weaponContainer;
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public OrchestraManager orchestraManager;

    private WeaponPlacer instantiatedWeaponPlacer;
    public Vehicle vehicle;

    [SerializeField] bool garageScene = false;
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
        /*if (garageScene)
        {
            weaponManager.SpawnButtonsForObtainedWeapons();
            weaponManager.SetGarageScene(true);
        }*/


        Debug.Log("Progress.Instance.playerInfo.vehicleSaveDatas[selectedVehicleId]: " + Progress.Instance.playerInfo.vehicleSaveDatas[selectedVehicleId]);
        Debug.Log("Progress.Instance.playerInfo.vehicleSaveDatas: " + Progress.Instance.playerInfo.vehicleSaveDatas.Count);
        vehicle.SetVehicleSaveData(Progress.Instance.playerInfo.vehicleSaveDatas[selectedVehicleId]);
        orchestraManager.SetVehicle(vehicle);
        if (garageScene)
        {
            weaponManager.SetGarageScene(true);
            weaponManager.SpawnButtonsForObtainedWeapons();
            weaponManager.SpawnWeaponsOnVehicle(vehicle, selectedVehicleId);
        }
        else
        {
            weaponManager.SpawnWeaponsOnVehicle(vehicle, selectedVehicleId);
        }
        //weaponManager.SpawnButtonsForObtainedWeapons();

    }
}
