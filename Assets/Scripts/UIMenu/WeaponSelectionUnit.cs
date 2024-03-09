using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUnit : MonoBehaviour
{
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public Button button;
    public int weaponId; //Progress.Instance.playerInfo.weaponSaveDatas[i].idVehicle

    public void SelectWeapon()
    {
        weaponManager.selectedWeaponId = weaponId;
        weaponManager.currentWeaponPlacer.previewWeaponCanBePlaced = true;
        weaponManager.currentWeaponPlacer.ChangePrefabModel();
        weaponManager.weaponRemover.enabled = false;
    }
}
