using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponReplacer : MonoBehaviour
{
    [SerializeField] Vehicle vehicle;
    [SerializeField] Weapon[] weapons;
    [SerializeField] GameObject[] turretsForReplacement;
    [SerializeField] GameObject[] cannonsForReplacement;
    [SerializeField] GameObject[] supportsForReplacement;
    [SerializeField] GameObject[] armoursForReplacement;
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] int bossBPM;

    private int turretCounter;
    private int cannonCounter;
    private int supportCounter;
    private int armourCounter;

    // For Testing
    public bool findWeapons = false;
    public bool replaceWeapons = false;

    // TODO:
    // Replace weapons by type, e.g. turret -> turret, machinegun -> machinegun (Done)
    // Maybe ddd "playing is allowed" bool to the weapon script (Done in Orchestra)
    // Add orchestra BPM reset functionality
    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (findWeapons)
        {
            findWeapons = false;
            FindAllWeapons();
        }
        if (replaceWeapons)
        {
            replaceWeapons = false;
            ReplaceWeaponsWithBossWeapons();
        }
    }

    private void FindAllWeapons()
    {
        //FindObjectsByType<Weapon>().
        weapons = vehicle.GetComponentsInChildren<Weapon>();
    }

    private void ReplaceWeaponsWithBossWeapons()
    {
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.weaponType)
            {
                case WeaponType.Turret:
                    ReplaceWeapon(turretsForReplacement, turretCounter);
                    turretCounter++;
                    break;
                case WeaponType.Cannon:
                    ReplaceWeapon(cannonsForReplacement, cannonCounter);
                    cannonCounter++;
                    break;
                case WeaponType.Support:
                    ReplaceWeapon(supportsForReplacement, supportCounter);
                    supportCounter++;
                    break;
                case WeaponType.Armour:
                    ReplaceWeapon(armoursForReplacement, armourCounter);
                    armourCounter++;
                    break;
                default:
                    //
                    break;
            }
        }

        // This action must be done after all of weapon have been replaced
        orchestraManager.SetBossBPM(bossBPM);
    }

    private void ReplaceWeapon(GameObject[] bossWeapons, int counter)
    {
        bossWeapons[counter].transform.SetParent(vehicle.weaponHolderParent.transform);
        bossWeapons[counter].transform.localPosition = weapons[0].gameObject.transform.parent.localPosition; // instead of weapons[0], it should be a corresponding boss weapon
        bossWeapons[counter].transform.localRotation = weapons[0].gameObject.transform.parent.localRotation;
        Weapon bossWeapon = bossWeapons[counter].GetComponentInChildren<Weapon>();
        bossWeapon.gameObject.transform.localRotation = weapons[0].gameObject.transform.localRotation;
        bossWeapon.isPlaced = true;
        weapons[0].gameObject.transform.parent.gameObject.SetActive(false);
        bossWeapons[counter].gameObject.SetActive(true);
    }
}
