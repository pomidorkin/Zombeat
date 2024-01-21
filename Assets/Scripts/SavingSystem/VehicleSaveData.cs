using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleSaveData : MonoBehaviour
{
    [SerializeField] GameObject vehiclePrefab;
    public int id;
    public bool obtained;
    //public bool upgraded = false;
}