using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRemover : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] VehicleWeaponInitializer vehicleWeaponInitializer;
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] WeaponPopupUI weaponPopupUI;
    [SerializeField] WeaponManager weaponManager;
    private Weapon weapon;
    int layerMask;

    RaycastHit hit;
    // TODO:
    // ќсвобождать слот тачки при удалении
    // ƒелать оружие снова доступном при удалении
    // ѕровер€ть, последнее ли это оружие оставшеес€ на тачке
    // ≈сли последнее, то обнул€ть тональность и BPM тачки
    // ћожет быть останавливать ќркестор

    private void Start()
    {
        layerMask = LayerMask.GetMask("Weapon");
    }

    void Update()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Check if the ray hits the cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.tag == "Weapon")
            {
                
                //Vector3 hitPoint = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    weapon = hit.transform.gameObject.GetComponentInChildren<Weapon>();
                    //weapon = hit.transform.gameObject.GetComponentInParent<Weapon>();
                    weaponPopupUI.gameObject.SetActive(true);
                    //weaponPopupUI.lookAt = weapon.gameObject.transform;
                    weaponPopupUI.lookAt = weapon.parentObject.gameObject.transform;
                    weaponPopupUI.attachedWeapon = weapon;
                    weaponPopupUI.damageText.text = "DMG: " + weapon.weaponDamage.ToString();
                    weaponPopupUI.DropdownHandler();
                }
               
                //RemoveWeapon(hit);
            }
        }
    }

    public void RemoveWeapon()
    {
        EmptyVehicleSlot(weapon, vehicleWeaponInitializer.vehicle);
        if (weapon.isWaveEffector && orchestraManager.waveEffectorWeapon ==  weapon)
        {
            orchestraManager.waveEffectorWeapon = null;
        }

        Destroy(weapon.parentObject.transform.gameObject);
        weaponManager.SpawnButtonsForObtainedWeapons();
    }

    private void EmptyVehicleSlot(Weapon weapon, Vehicle vehicle)
    {
        bool lastSlot = true;
        bool slotEmptied = false;
        for (int i = 0; i < vehicle.weaponSlots.Length; i++)
        {
            Debug.Log("Checking Slot...");
            if (vehicle.weaponSlots[i].weapon == weapon && vehicle.weaponSlots[i].weaponTypeSlot == weapon.weaponType && vehicle.weaponSlots[i].occupied)
            {
                Debug.Log("My type, slot occupied...");
                if (!slotEmptied)
                {
                    vehicle.weaponSlots[i].occupied = false;
                    weapon.isPlaced = false;
                    weapon.weaponSaveData.placed = false;
                    weapon.weaponSaveData.preferredEnemy = EnemyType.Default;
                    weapon.weaponSaveData.weaponSoundVolume = 1.0f;
                    slotEmptied = true;
                    Debug.Log("Emptying slot..." + ", Weapon id: " + weapon.weaponSaveData.id);
                }
                if (vehicle.weaponSlots[i].occupied)
                {
                    lastSlot = false;
                }
                //Progress.Instance.playerInfo.weaponSaveDatas[0].placed = false;
                //break;
            }
        }
        if (lastSlot)
        {
            vehicle.ResetSoundSettings();
            orchestraManager.mainBPM = 0;
            orchestraManager.playingAllowed = false;
            orchestraManager.allEnemiesManager.eventsAllowed = false;
        }
        vehicleWeaponInitializer.UpdateSlots();
        Progress.Instance.Save();
        Debug.Log("Saving...");
    }
}
