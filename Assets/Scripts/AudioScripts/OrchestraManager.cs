using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundUnitKey
{
    A_SHARP_M,
    D_SHARP_M
}

public class OrchestraManager : MonoBehaviour
{
    public delegate void PlayMusicAction();
    public event PlayMusicAction OnMusicPlayed;

    public delegate void VehicleSetAction();
    public event VehicleSetAction OnVehicleSet;


    public int mainBPM;
    [SerializeField] int numberOfTacts = 8; // How many tacts you wait before you start playing
    [SerializeField] public Vehicle chosenVehicle;
    //public SoundUnitKey currentMusicKey;
    //public bool keySpecified = false;
    private float counter = 0;
    private float triggerValue;

    private void OnEnable()
    {
        triggerValue = ((float)(60f / mainBPM) * numberOfTacts);
    }

    private void Update()
    {
        if (chosenVehicle != null)
        {
            counter += Time.deltaTime;
            if (counter >= triggerValue)
            {
                counter = 0;
                OnMusicPlayed();
                Debug.Log("VEvent trigger: OnMusicPlayed()");
            }
        }
    }

    public void SetVehicle(Vehicle vehicle)
    {
        chosenVehicle = vehicle;
        mainBPM = vehicle.vehicleMainBPM;
        triggerValue = ((float)(60f / mainBPM) * numberOfTacts);
        OnVehicleSet();
    }
}