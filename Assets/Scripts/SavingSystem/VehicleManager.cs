using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] VehicleWeaponInitializer vehicleWeaponInitializer;
    [SerializeField] MyScrollSnap scrollSnap;

    public int selectedVehicleId = 0;

    private void Start()
    {
        selectedVehicleId = Progress.Instance.playerInfo.selectedVehicleId;
        SpawnPanelsForObtainedVehicles();
    }

    public void SpawnVehice()
    {
        vehicleWeaponInitializer.SelectVehicle(selectedVehicleId);
    }

    public void SpawnPanelsForObtainedVehicles()
    {
        if (scrollSnap.scrollSnap.Content.childCount > 0)
        {
            for (int i = 0; i < scrollSnap.scrollSnap.Content.childCount; i++)
            {
                scrollSnap.Remove(i);
            }
        }

        for (int i = 0; i < Progress.Instance.playerInfo.vehicleSaveDatas.Count; i++)
        {
            if (Progress.Instance.playerInfo.vehicleSaveDatas[i].obtained)
            {
                scrollSnap.AddToBack();
            }
        }
        scrollSnap.scrollSnap.GoToPanel(selectedVehicleId);
    }
}
