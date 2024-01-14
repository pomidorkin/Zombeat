using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSaveData
{
    //[SerializeField] GameObject weaponPrefab;
    public int id;
    public bool obtained;
    public bool placed;
    public int idVehicle;
    // Position on a car;
    public Vector3 position;
    public Quaternion rotation;
    public float childRotationY;
}