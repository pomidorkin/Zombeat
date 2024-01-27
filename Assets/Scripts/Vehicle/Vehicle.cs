using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] public WeaponSlot[] weaponSlots;
    [SerializeField] public GameObject weaponHolderParent;
    public SoundUnitKey vehicleMainKey;
    public int vehicleMainBPM;
    public bool keySpecified = false;
    public VehicleSaveData vehicleSaveData;

    public void SetVehicleSaveData(VehicleSaveData vehicleSaveData)
    {
        this.vehicleSaveData = vehicleSaveData;
        SetSoundSettings();
    }

    private void SetSoundSettings()
    {
        vehicleMainKey = vehicleSaveData.vehicleMainKey;
        vehicleMainBPM = vehicleSaveData.vehicleMainBPM;
        keySpecified = vehicleSaveData.keySpecified;
    }

    public void SaveSoundSettings()
    {
        vehicleSaveData.vehicleMainKey = vehicleMainKey;
        vehicleSaveData.vehicleMainBPM = vehicleMainBPM;
        vehicleSaveData.keySpecified = keySpecified;
        Progress.Instance.Save();
    }

}
