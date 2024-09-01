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
    [SerializeField] public float health; // Базовое здоровье
    private float currentHealth;
    [SerializeField] public int healthIncrementValue; // На сколько увеличивается здоровье при апргейде
    [SerializeField] public int timesHealthUnpgraded; // Сколько раз было улучшенно здоровье
    [SerializeField] public int speed;
    [SerializeField] public int speedIncrementValue;
    [SerializeField] public int timesSpeedUnpgraded;
    [SerializeField] public Sprite carImage;
    [SerializeField] public Transform enemyAimPoint;
    private VehicleHealthUI vehicleHealthUI;

    private void Start()
    {
        currentHealth = health;
    }

    public void DealDamageToVehicle(float val)
    {
        currentHealth -= val;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Vehicle Destroyed");
        }

        if (vehicleHealthUI != null)
        {
            vehicleHealthUI.SetHealthBarValue(health, currentHealth);
            Debug.Log("Current vehicle health: " + currentHealth);
        }
    }

    public void SetHealthIU(VehicleHealthUI vehicleHealthUI)
    {
        this.vehicleHealthUI = vehicleHealthUI;
    }

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
