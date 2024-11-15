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
    [SerializeField] public BalanceManager balanceManager;
    [SerializeField] public NotificationsManager notificationsManager;
    [SerializeField] public VehicleClassManager vehicleClassManager;
    public List<WeaponSelectionUnit> weaponSelectionUnits;
    public WeaponPlacer currentWeaponPlacer;
    private bool garageScene = false;

    public int selectedWeaponId = 0;

    // MarketScrollSnap
    [SerializeField] WeaponScrollSnap weaponScrollSnap;
    public bool firsClassSelected = true; // toggles for all weapon classes
    public bool secondClassSelected = false;
    public bool thirdClassSelected = false;
    public bool fourthClassSelected = false;

    // InventoryScrollSnap
    [SerializeField] InventoryScrollSnap inventoryScrollSnap;
    public bool inventoryClassOneSelected = true; // toggles for all weapon classes (inventory)
    public bool inventoryClassTwoSelected = false;
    public bool inventoryClassThreeSelected = false;
    public bool inventoryClassFourSelected = false;

    private void Start()
    {
        //SpawnButtonsForObtainedWeapons();
        //weaponSelectionUnits = new List<WeaponSelectionUnit>();
        //SpawnWeaponsOnVehicle(vehicle);
    }

    /*public void SpawnButtonsForObtainedWeapons()
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
    }*/

    public void SpawnWeaponsOnVehicle(Vehicle vehicle, int currVehicleId)
    {
        // ������ ����, ��� �������� ����������� ������

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
                        if (slot.weaponTypeSlot == spawnedWeapon.weaponType && !slot.occupied)
                        {
                            slot.occupied = true;
                            slot.weapon = spawnedWeapon;
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
        if (garageScene)
        {
            weaponRemover.enabled = true;
        }
        //weaponRemover.enabled = true;
    }

    public void SetGarageScene(bool val)
    {
        garageScene = val;
    }

    public void SpawnPanelsForWeapons()
    {
        if (weaponScrollSnap.scrollSnap.Content.childCount > 0)
        {
            int val = weaponScrollSnap.scrollSnap.Content.childCount;
            for (int i = 0; i < val; i++)
            {
                weaponScrollSnap.Remove(i);
            }
        }

        for (int i = 0; i < weaponContainer.weaponPrefabs.Length; i++)
        {
            if (!Progress.Instance.playerInfo.weaponSaveDatas[i].obtained) // Should be NOT OBTAINED insted. I left OBTAINED for testing
            {
                Weapon weapon = weaponContainer.weaponPrefabs[i].GetComponentInChildren<Weapon>();
                /*if (weapon.weaponType == WeaponType.Turret)
                {
                    if (turretSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Cannon)
                {
                    if (cannonSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Support)
                {
                    if (supportSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Armour)
                {
                    if (armourSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }*/
                if (VehicleClass.CheckClass(weapon.soundUnit.GetSoundUnitKey(), weapon.soundUnit.GetSoundUnitBPM()) == VehicleClass.VehicleClassType.NEONVORTEX)
                {
                    if (firsClassSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                if (VehicleClass.CheckClass(weapon.soundUnit.GetSoundUnitKey(), weapon.soundUnit.GetSoundUnitBPM()) == VehicleClass.VehicleClassType.HEAVYDUTY)
                {
                    if (secondClassSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                /*if (weapon.weaponType == WeaponType.Support)
                {
                    if (thirdClassSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Armour)
                {
                    if (fourthClassSelected)
                    {
                        weaponScrollSnap.AddCustom(i);
                    }
                }*/
                //weaponScrollSnap.AddToBack();
            }
        }
        //weaponScrollSnap.scrollSnap.GoToPanel(selectedVehicleId);
    }

    public void SpawnButtonsForObtainedWeapons()
    {

        if (inventoryScrollSnap.scrollSnap.Content.childCount > 0)
        {
            int val = inventoryScrollSnap.scrollSnap.Content.childCount;
            for (int i = 0; i < val; i++)
            {
                inventoryScrollSnap.Remove(i);
            }
        }

        for (int i = 0; i < Progress.Instance.playerInfo.weaponSaveDatas.Count; i++)
        {
            if (Progress.Instance.playerInfo.weaponSaveDatas[i].obtained && !Progress.Instance.playerInfo.weaponSaveDatas[i].placed)
            {
                Weapon weapon = weaponContainer.weaponPrefabs[i].GetComponentInChildren<Weapon>();
                if (VehicleClass.CheckClass(weapon.soundUnit.GetSoundUnitKey(), weapon.soundUnit.GetSoundUnitBPM()) == VehicleClass.VehicleClassType.NEONVORTEX)
                {
                    if (inventoryClassOneSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }
                if (VehicleClass.CheckClass(weapon.soundUnit.GetSoundUnitKey(), weapon.soundUnit.GetSoundUnitBPM()) == VehicleClass.VehicleClassType.HEAVYDUTY)
                {
                    if (inventoryClassTwoSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }/*
                if (weapon.weaponType == WeaponType.Turret)
                {
                    if (inventoryTurretSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Cannon)
                {
                    if (inventoryCannonSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Support)
                {
                    if (inventorySupportSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }
                if (weapon.weaponType == WeaponType.Armour)
                {
                    if (inventoryArmourSelected)
                    {
                        inventoryScrollSnap.AddCustom(i);
                    }
                }*/
            }
        }
    }

    public void SelectTurret()
    {
        firsClassSelected = true;
        secondClassSelected = false;
        thirdClassSelected = false;
        fourthClassSelected = false;
        SpawnPanelsForWeapons();
    }
    public void SelectCannon()
    {
        Debug.Log(", childCount: " + weaponScrollSnap.scrollSnap.Content.childCount);
        secondClassSelected = true;
        firsClassSelected = false;
        thirdClassSelected = false;
        fourthClassSelected = false;
        SpawnPanelsForWeapons();
    }
    public void SelectSupport()
    {
        thirdClassSelected = true;
        firsClassSelected = false;
        secondClassSelected = false;
        fourthClassSelected = false;
        SpawnPanelsForWeapons();
    }
    public void SelectArmour()
    {
        fourthClassSelected = true;
        firsClassSelected = false;
        secondClassSelected = false;
        thirdClassSelected = false;
        SpawnPanelsForWeapons();
    }

    public void BuyWeapon(int weaponId, int price)
    {
        if (balanceManager.DeductCoins(price))
        {
            Progress.Instance.playerInfo.weaponSaveDatas[weaponId].obtained = true;
            Progress.Instance.Save();
        }
        SpawnPanelsForWeapons();
    }

    public void SelectInventoryTurret()
    {
        inventoryClassOneSelected = true;
        inventoryClassTwoSelected = false;
        inventoryClassThreeSelected = false;
        inventoryClassFourSelected = false;
        SpawnButtonsForObtainedWeapons();
    }
    public void SelectInventoryCannon()
    {
        Debug.Log(", childCount: " + weaponScrollSnap.scrollSnap.Content.childCount);
        inventoryClassTwoSelected = true;
        inventoryClassOneSelected = false;
        inventoryClassThreeSelected = false;
        inventoryClassFourSelected = false;
        SpawnButtonsForObtainedWeapons();
    }
    public void SelectInventorySupport()
    {
        inventoryClassThreeSelected = true;
        inventoryClassOneSelected = false;
        inventoryClassTwoSelected = false;
        inventoryClassFourSelected = false;
        SpawnButtonsForObtainedWeapons();
    }
    public void SelectInventoryArmour()
    {
        inventoryClassFourSelected = true;
        inventoryClassOneSelected = false;
        inventoryClassTwoSelected = false;
        inventoryClassThreeSelected = false;
        SpawnButtonsForObtainedWeapons();
    }

    public void CancelWeaponPlacement()
    {
        currentWeaponPlacer.AbortPlacement();
    }
}