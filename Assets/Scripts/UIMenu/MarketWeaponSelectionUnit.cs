using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;

public class MarketWeaponSelectionUnit : MonoBehaviour
{
    [SerializeField] public WeaponContainer weaponContainer;
    [SerializeField] public Image weaponImage;
    //[SerializeField] public ProgressBar carHealthProgressBar;
    //[SerializeField] public ProgressBar carSpeedProgressBar;
    [SerializeField] public TMP_Text weaponName;
    [SerializeField] public int weaponPrice;
    [SerializeField] public Button buyButton;
    public WeaponManager weaponManager;
    public int weaponId; //Progress.Instance.playerInfo.weaponSaveDatas[i].idVehicle

    public void BuyHandler()
    {
        weaponManager.BuyWeapon(weaponId, weaponPrice);
    }

}
