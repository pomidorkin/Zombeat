using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Button selectionButton;
    [SerializeField] Canvas canvas;

    [SerializeField] WeaponContainer USED_FOR_TESTING;

    private void Start()
    {
        SpawnButtonsForObtainedWeapons();
        TEST();
    }

    private void SpawnButtonsForObtainedWeapons()
    {
        foreach (WeaponSaveData weaponSD in Progress.Instance.playerInfo.weaponSaveDatas)
        {
            if (weaponSD.obtained)
            {
                Button newButton = Instantiate(selectionButton) as Button;
                newButton.transform.SetParent(canvas.transform, false);
            }
        }
    }

    public void TEST()
    {
        // Привер того, как спавнить сохранённое оружие
        // Надо спавнить на автомобиле, как child object
        // Также надо при спавне обозначать, что слоты у тачки заняты
        // Плюс, почему-то заспавненное оружие не стреляет
        // Баг с isPlaced у того оружие, которое перетаскивается. Оно
        // тоже начинает стрелять почему-то
        foreach (WeaponSaveData weaponSD in Progress.Instance.playerInfo.weaponSaveDatas)
        {
            if (weaponSD.placed)
            {
                GameObject spawnedWeaponObject = Instantiate(USED_FOR_TESTING.weaponPrefabs[weaponSD.id], weaponSD.position, weaponSD.rotation);
                Weapon spawnedWeapon = spawnedWeaponObject.GetComponentInChildren<Weapon>();
                spawnedWeapon.gameObject.transform.localRotation = Quaternion.Euler(0, weaponSD.childRotationY, 0);
                spawnedWeapon.isPlaced = true;
            }
        }
    }
}
