using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleClassManager : MonoBehaviour
{
    [SerializeField] TMP_Text classText;

    public void UpdateClassText(Vehicle vehicle)
    {
        classText.text = VehicleClass.CheckClass(vehicle.vehicleMainKey, vehicle.vehicleMainBPM).ToString();
    }
}
