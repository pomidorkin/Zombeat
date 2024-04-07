using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionUnit : MonoBehaviour
{
    [SerializeField] public VehicleManager vehicleManager;
    //[SerializeField] public Button button;
    public int vehicleId; //Progress.Instance.playerInfo.weaponSaveDatas[i].idVehicle

    public void SelectWeapon()
    {
        vehicleManager.selectedVehicleId = vehicleId;
    }
}
