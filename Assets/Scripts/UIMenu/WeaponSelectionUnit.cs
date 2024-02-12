using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionUnit : MonoBehaviour
{
    [SerializeField] public WeaponManager weaponManager;
    public int weaponId;

    public void SelectWeapon()
    {
        weaponManager.selectedWeaponId = weaponId;
        weaponManager.currentWeaponPlacer.previewWeaponCanBePlaced = true;
        weaponManager.currentWeaponPlacer.ChangePrefabModel();
        weaponManager.weaponRemover.enabled = false;
    }
}
