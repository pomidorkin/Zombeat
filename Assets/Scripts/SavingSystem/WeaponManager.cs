using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Button selectionButton;
    [SerializeField] Canvas canvas;

    [SerializeField] WeaponContainer weaponContainer;
    [SerializeField] OrchestraManager orchestraManager;

    public int selectedWeaponId = 0;

    private void Start()
    {
        SpawnButtonsForObtainedWeapons();
        //SpawnWeaponsOnVehicle(vehicle);
    }

    private void SpawnButtonsForObtainedWeapons()
    {
        foreach (WeaponSaveData weaponSD in Progress.Instance.playerInfo.weaponSaveDatas)
        {
            if (weaponSD.obtained)
            {
                Button newButton = Instantiate(selectionButton) as Button;
                newButton.transform.SetParent(canvas.transform, false);
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
    }
}