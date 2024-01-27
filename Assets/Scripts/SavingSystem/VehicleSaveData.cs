using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleSaveData
{
    [SerializeField] GameObject vehiclePrefab;
    public int id;
    public bool obtained;
    public SoundUnitKey vehicleMainKey;
    public int vehicleMainBPM;
    public bool keySpecified = false;
    //public bool upgraded = false;
}