using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWeaponInitializer : MonoBehaviour
{
    public delegate void VehicleSpawnAction();
    public event VehicleSpawnAction OnVehicleSpawned;

    [SerializeField] VehicleContainer vehicleContainer;
    [SerializeField] WeaponContainer weaponContainer;
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public OrchestraManager orchestraManager;
    [SerializeField] private SlotManager slotManager;
    [SerializeField] public VehicleClassManager vehicleClassManager;

    [SerializeField] Transform vehicleSpawnPosition;

    private WeaponPlacer instantiatedWeaponPlacer;
    public Vehicle vehicle;

    [SerializeField] bool garageScene = false;
    //SoundUnitKey currentVehicleKey;
    //int currentVehicleBPM;

    public int selectedVehicleId; // Currently selected id

    private void Start()
    {
        selectedVehicleId = Progress.Instance.playerInfo.selectedVehicleId;
        GameObject instantiatedVehicle = Instantiate(vehicleContainer.vehiclePrefabs[selectedVehicleId].gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instantiatedWeaponPlacer = instantiatedVehicle.GetComponentInChildren<WeaponPlacer>();
        vehicle = instantiatedWeaponPlacer.vehicle;
        if (!garageScene)
        {
            OnVehicleSpawned();
        }
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
            //vehicle.carController.enabled = false;
            vehicle.carController.canControl = false;
        }
        else
        {
            weaponManager.SpawnWeaponsOnVehicle(vehicle, selectedVehicleId);
        }
        if (garageScene)
        {
            slotManager.SetVehicle(vehicle);
            vehicleClassManager.UpdateClassText(vehicle);
        }
        instantiatedVehicle.GetComponent<Rigidbody>().Move(new Vector3(vehicleSpawnPosition.position.x, vehicleSpawnPosition.position.y + 0.1f, vehicleSpawnPosition.position.z), Quaternion.identity);

    }

    public void SelectVehicle(int id)
    {
        if (vehicle != null)
        {
            Destroy(vehicle.gameObject);
            slotManager.ClearAllSlotsContainters();
        }
        orchestraManager.playingAllowed = false;
        Progress.Instance.playerInfo.selectedVehicleId = id;
        selectedVehicleId = id;
        GameObject instantiatedVehicle = Instantiate(vehicleContainer.vehiclePrefabs[selectedVehicleId].gameObject, new Vector3(0, 0.1f, 0), Quaternion.identity);
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
            //vehicle.carController.enabled = false;
            vehicle.carController.canControl = false;
        }
        else
        {
            weaponManager.SpawnWeaponsOnVehicle(vehicle, selectedVehicleId);
        }

        slotManager.SetVehicle(vehicle);
        vehicleClassManager.UpdateClassText(vehicle);
        instantiatedVehicle.GetComponent<Rigidbody>().Move(new Vector3(vehicleSpawnPosition.position.x, vehicleSpawnPosition.position.y + 0.1f, vehicleSpawnPosition.position.z), Quaternion.identity);
        Progress.Instance.Save();
    }

    public void UpdateSlots()
    {
        slotManager.ClearAllSlotsContainters();
        slotManager.SetVehicle(vehicle);
    }
}
