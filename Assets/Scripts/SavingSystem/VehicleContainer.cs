using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleContainer : MonoBehaviour
{
    //[SerializeField] public GameObject[] vehiclePrefabs;
    [SerializeField] GameObject carObtinedUi;
    [SerializeField] RectTransform parentForUIModel;
    [SerializeField] public Vehicle[] vehiclePrefabs;

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

    public void ObtainNewVehicle(int vehicleId)
    {
        if (!Progress.Instance.playerInfo.vehicleSaveDatas[vehicleId].obtained)
        {
            Progress.Instance.playerInfo.vehicleSaveDatas[vehicleId].obtained = true;
            Progress.Instance.Save();
            Debug.Log("New vehicle obtained! Vehicle id: " + vehicleId);
            if (carObtinedUi != null)
            {
                carObtinedUi.SetActive(true);
                Create3DObject(vehicleId);
            }
        }
    }

    public void Create3DObject(int vehicleId)
    {
        Debug.Log("Create3DObject");
        // Instantiate the prefab
        GameObject instance = Instantiate(vehiclePrefabs[vehicleId].vehicleModelForUI);

        // Set the parent to the specified RectTransform
        instance.transform.SetParent(parentForUIModel, false); // Use 'false' to keep local scale

        // Rescale the object to 100
        instance.transform.localScale = new Vector3(100, 100, 100);

        instance.transform.localPosition = new Vector3(0, -150, 0);
    }
}