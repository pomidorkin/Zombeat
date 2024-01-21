using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Button selectionButton;
    [SerializeField] Canvas canvas;

    [SerializeField] WeaponContainer weaponContainer;

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
        // Плюс, почему-то заспавненное оружие не стреляет
        // Баг с isPlaced у того оружие, которое перетаскивается. Оно
        // тоже начинает стрелять почему-то
        foreach (WeaponSaveData weaponSD in Progress.Instance.playerInfo.weaponSaveDatas)
        {
            if (currVehicleId == weaponSD.idVehicle)
            {
                if (weaponSD.placed)
                {
                    GameObject spawnedWeaponObject = Instantiate(weaponContainer.weaponPrefabs[weaponSD.id], weaponSD.position, weaponSD.rotation);
                    spawnedWeaponObject.transform.SetParent(vehicle.weaponHolderParent.transform);
                    Weapon spawnedWeapon = spawnedWeaponObject.GetComponentInChildren<Weapon>();
                    spawnedWeapon.gameObject.transform.localRotation = Quaternion.Euler(0, weaponSD.childRotationY, 0);
                    spawnedWeapon.isPlaced = true;
                    foreach (WeaponSlot slot in vehicle.weaponSlots)
                    {
                        if (slot.weaponTypeSlot == spawnedWeapon.weaponType)
                        {
                            slot.occupied = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}