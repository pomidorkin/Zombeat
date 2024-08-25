using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    [SerializeField] public WeaponSlot[] weaponSlots;
    [SerializeField] public GameObject weaponHolderParent;
    [SerializeField] public RCC_CarControllerV3 carController;
    public SoundUnitKey vehicleMainKey;
    public int vehicleMainBPM;
    public bool keySpecified = false;
    public VehicleSaveData vehicleSaveData;
    [SerializeField] public int health; // ������� ��������
    [SerializeField] public int healthIncrementValue; // �� ������� ������������� �������� ��� ��������
    [SerializeField] public int timesHealthUnpgraded; // ������� ��� ���� ��������� ��������
    [SerializeField] public int speed;
    [SerializeField] public int speedIncrementValue;
    [SerializeField] public int timesSpeedUnpgraded;
    [SerializeField] public Sprite carImage;
    [SerializeField] public Transform enemyAimPoint;

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

    public void ResetSoundSettings()
    {
        keySpecified = false;
        vehicleMainBPM = 0;
        vehicleSaveData.vehicleMainKey = vehicleMainKey;
        vehicleSaveData.vehicleMainBPM = vehicleMainBPM;
        vehicleSaveData.keySpecified = keySpecified;
        Progress.Instance.Save();

    }

}
