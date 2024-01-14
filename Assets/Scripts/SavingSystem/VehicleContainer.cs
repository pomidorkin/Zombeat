using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleContainer : MonoBehaviour
{
    [SerializeField] VehicleSaveData[] vehicleSaveDatas;

    private void Start()
    {
        /*if (Progress.Instance.playerInfo.vehicles.keys.Count < 1)
        {
            Progress.Instance.playerInfo.vehicles.Initialize();
            List<int> keyList = new List<int>();
            List<bool> valueList = new List<bool>();
            for (int i = 0; i < vehicleSaveDatas.Length; i++)
            {
                keyList.Add(i);
                valueList.Add(false);
            }
            Progress.Instance.playerInfo.vehicles.keys = keyList;
            Progress.Instance.playerInfo.vehicles.values = valueList;
            Progress.Instance.playerInfo.vehicles.SetValue(0, true); // Making the first weapon accessible
            Progress.Instance.Save();
        }

        for (int i = 0; i < vehicleSaveDatas.Length; i++)
        {
            vehicleSaveDatas[i].id = i;
        }*/
    }
}