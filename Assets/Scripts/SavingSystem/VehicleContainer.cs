using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleContainer : MonoBehaviour
{
    [SerializeField] public GameObject[] vehiclePrefabs;

    private void Start()
    {
        Debug.Log("Progress.Instance.playerInfo.weaponSaveDatas.Count" + Progress.Instance.playerInfo.weaponSaveDatas.Count);
        if (Progress.Instance.playerInfo.vehicleSaveDatas.Count < 1)
        {
            for (int i = 0; i < vehiclePrefabs.Length; i++)
            {
                VehicleSaveData newVehicleSaveData = new VehicleSaveData();
                newVehicleSaveData.id = i;
                newVehicleSaveData.obtained = false;
                Progress.Instance.playerInfo.vehicleSaveDatas.Add(newVehicleSaveData);
            }
            //Progress.Instance.playerInfo.weapons.SetValue(0, true); // Making the first weapon accessible
            Progress.Instance.Save();
            Debug.Log("Progress.Instance.playerInfo.vehicleSaveDatas.Count: " + Progress.Instance.playerInfo.vehicleSaveDatas.Count);
        }
    }
}