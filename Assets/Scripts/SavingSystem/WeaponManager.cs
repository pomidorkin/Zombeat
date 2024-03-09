using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponSelectionUnit selectionButton;
    [SerializeField] GameObject parentHolder;

    [SerializeField] WeaponContainer weaponContainer;
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] public WeaponRemover weaponRemover;
    public List<WeaponSelectionUnit> weaponSelectionUnits;
    public WeaponPlacer currentWeaponPlacer;

    public int selectedWeaponId = 0;

    private void Start()
    {
        SpawnButtonsForObtainedWeapons();
        //weaponSelectionUnits = new List<WeaponSelectionUnit>();
        //SpawnWeaponsOnVehicle(vehicle);
    }

    public void SpawnButtonsForObtainedWeapons()
    {
        if (weaponSelectionUnits != null)
        {
            if (weaponSelectionUnits.Count > 0)
            {
                foreach (WeaponSelectionUnit unit in weaponSelectionUnits)
                {
                    Destroy(unit.gameObject);
                }
                weaponSelectionUnits.Clear();
            }
        }
        else
        {
            weaponSelectionUnits = new List<WeaponSelectionUnit>();
        }

        for (int i = 0; i < Progress.Instance.playerInfo.weaponSaveDatas.Count; i++)
        {
            if (Progress.Instance.playerInfo.weaponSaveDatas[i].obtained && !Progress.Instance.playerInfo.weaponSaveDatas[i].placed)
            {
                WeaponSelectionUnit newButton = Instantiate(selectionButton);
                newButton.transform.SetParent(parentHolder.transform, false);
                newButton.weaponId = i;
                newButton.weaponManager = this;
                weaponSelectionUnits.Add(newButton);
                if ((orchestraManager.chosenVehicle.keySpecified == true) && ((orchestraManager.chosenVehicle.vehicleMainKey != weaponContainer.weaponPrefabs[i].GetComponentInChildren<Weapon>().soundUnit.GetSoundUnitKey()) || (orchestraManager.chosenVehicle.vehicleMainBPM != weaponContainer.weaponPrefabs[i].GetComponentInChildren<Weapon>().soundUnit.GetSoundUnitBPM())))
                {
                    newButton.button.interactable = false;
                    Debug.Log("orchestraManager.chosenVehicle.keySpecified: " + orchestraManager.chosenVehicle.keySpecified);
                }
                Debug.Log("Spawning button...");
            }
        }
    }

    public void SpawnWeaponsOnVehicle(Vehicle vehicle, int currVehicleId)
    {
        // Пример того, как спавнить сохранённое оружие

        for (int i = 0; i < Progress.Instance.playerInfo.weaponSaveDatas.Count; i++)
        {
            if (Progress.Instance.playerInfo.weaponSaveDatas[i].placed)
            {
                if (currVehicleId == Progress.Instance.playerInfo.weaponSaveDatas[i].idVehicle)
                {
                    GameObject spawnedWeaponObject = Instantiate(weaponContainer.weaponPrefabs[Progress.Instance.playerInfo.weaponSaveDatas[i].id], Progress.Instance.playerInfo.weaponSaveDatas[i].position, Progress.Instance.playerInfo.weaponSaveDatas[i].rotation);
                    spawnedWeaponObject.transform.SetParent(vehicle.weaponHolderParent.transform);
                    Weapon spawnedWeapon = spawnedWeaponObject.GetComponentInChildren<Weapon>();
                    spawnedWeapon.weaponSaveData = Progress.Instance.playerInfo.weaponSaveDatas[i];
                    spawnedWeapon.gameObject.transform.localRotation = Quaternion.Euler(0, Progress.Instance.playerInfo.weaponSaveDatas[i].childRotationY, 0);
                    spawnedWeapon.isPlaced = true;
                    foreach (WeaponSlot slot in vehicle.weaponSlots)
                    {
                        if (slot.weaponTypeSlot == spawnedWeapon.weaponType)
                        {
                            slot.occupied = true;
                            if (!orchestraManager.playingAllowed)
                            {
                                orchestraManager.playingAllowed = true;
                                orchestraManager.allEnemiesManager.eventsAllowed = true;
                                orchestraManager.allEnemiesManager.TriggerEvent();
                            }
                            break;
                        }
                    }
                }
            }
        }
        weaponRemover.enabled = true;
    }
}