using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotManager : MonoBehaviour
{
    private Vehicle vehicle;
    private List<SlotUIUnit> allSlotsContainer;

    [SerializeField] GameObject turretSlotUnit;
    [SerializeField] GameObject cannonSlotUnit;
    [SerializeField] GameObject armourSlotUnit;
    [SerializeField] GameObject supporSlotUnit;

    [SerializeField] GameObject turretsContainerUIElement;
    [SerializeField] GameObject cannonsContainerUIElement;
    [SerializeField] GameObject armoursContainerUIElement;
    [SerializeField] GameObject supportsContainerUIElement;

    GameObject newSlotUnit;
    SlotUIUnit slotUIUnit;

    private void OnEnable()
    {
        allSlotsContainer = new List<SlotUIUnit>();
    }
    public void SetVehicle(Vehicle vehicle)
    {
        this.vehicle = vehicle;
        foreach (WeaponSlot slot in vehicle.weaponSlots)
        {
            switch (slot.weaponTypeSlot)
            {
                 
                case WeaponType.Turret:
                    newSlotUnit = Instantiate(turretSlotUnit, turretsContainerUIElement.transform);
                    slotUIUnit = newSlotUnit.GetComponent<SlotUIUnit>();
                    allSlotsContainer.Add(slotUIUnit);
                    if (slot.occupied)
                    {
                        Debug.Log("Slot Occupied");
                        slotUIUnit.SetWeaponImageVisible(true);
                        slotUIUnit.SetWeaponImage(slot.weapon.weaponImage);
                    }
                    break;
                case WeaponType.Cannon:
                    newSlotUnit = Instantiate(cannonSlotUnit, cannonsContainerUIElement.transform);
                    slotUIUnit = newSlotUnit.GetComponent<SlotUIUnit>();
                    allSlotsContainer.Add(slotUIUnit);
                    if (slot.occupied)
                    {
                        slotUIUnit.SetWeaponImageVisible(true);
                        slotUIUnit.SetWeaponImage(slot.weapon.weaponImage);
                    }
                    break;
                case WeaponType.Armour:
                    newSlotUnit = Instantiate(armourSlotUnit, armoursContainerUIElement.transform);
                    slotUIUnit = newSlotUnit.GetComponent<SlotUIUnit>();
                    allSlotsContainer.Add(slotUIUnit);
                    if (slot.occupied)
                    {
                        slotUIUnit.SetWeaponImageVisible(true);
                        slotUIUnit.SetWeaponImage(slot.weapon.weaponImage);
                    }
                    break;
                case WeaponType.Support:
                    newSlotUnit = Instantiate(supporSlotUnit, supportsContainerUIElement.transform);
                    slotUIUnit = newSlotUnit.GetComponent<SlotUIUnit>();
                    allSlotsContainer.Add(slotUIUnit);
                    if (slot.occupied)
                    {
                        slotUIUnit.SetWeaponImageVisible(true);
                        slotUIUnit.SetWeaponImage(slot.weapon.weaponImage);
                    }
                    break;
                default:
                    Debug.Log("none of the above");
                    break;
            }
        }
    }

    public void ClearAllSlotsContainters()
    {
        foreach (SlotUIUnit item in allSlotsContainer)
        {
            item.SetWeaponImageVisible(false);
            Destroy(item.gameObject);
        }
        allSlotsContainer.Clear();
    }
}