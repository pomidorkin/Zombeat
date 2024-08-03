using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public WeaponType weaponTypeSlot;
    public bool occupied = false;
    public Weapon weapon;
}