using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;

public class VehicleSelectionUnit : MonoBehaviour
{
    [SerializeField] public VehicleManager vehicleManager;
    [SerializeField] public Image carImage;
    [SerializeField] public ProgressBar carHealthProgressBar;
    [SerializeField] public ProgressBar carSpeedProgressBar;
    [SerializeField] TMP_Text carName;
    //[SerializeField] public Button button;
    public int vehicleId; //Progress.Instance.playerInfo.vehicleSaveDatas[i].idVehicle

    public void SelectVehicle()
    {
        vehicleManager.selectedVehicleId = vehicleId;
    }
}
