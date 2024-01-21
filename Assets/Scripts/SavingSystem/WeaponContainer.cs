using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] public GameObject[] weaponPrefabs;

    private void Start()
    {
        Debug.Log("Progress.Instance.playerInfo.weaponSaveDatas.Count" + Progress.Instance.playerInfo.weaponSaveDatas.Count);
        if (Progress.Instance.playerInfo.weaponSaveDatas.Count < 1)
        {
            for (int i = 0; i < weaponPrefabs.Length; i++)
            {
                WeaponSaveData newWeaponSaveData = new WeaponSaveData();
                newWeaponSaveData.id = i;
                newWeaponSaveData.obtained = false;
                Progress.Instance.playerInfo.weaponSaveDatas.Add(newWeaponSaveData);
            }
            //Progress.Instance.playerInfo.weapons.SetValue(0, true); // Making the first weapon accessible
            Progress.Instance.Save();
            Debug.Log("Progress.Instance.playerInfo.weaponSaveDatas.Count: " + Progress.Instance.playerInfo.weaponSaveDatas.Count);
        }
    }
}
