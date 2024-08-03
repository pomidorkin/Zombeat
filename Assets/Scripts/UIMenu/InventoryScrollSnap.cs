using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class InventoryScrollSnap : MonoBehaviour
{
    [SerializeField] private WeaponSelectionUnit panelPrefab;
    [SerializeField] private Toggle togglePrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private InputField addInputField, removeInputField;
    [SerializeField] public SimpleScrollSnap scrollSnap;
    //[SerializeField] public VehicleManager vehicleManager;
    [SerializeField] private WeaponContainer weaponContainer;
    [SerializeField] private WeaponManager weaponManager;

    private float toggleWidth;

    private void Awake()
    {
        toggleWidth = (togglePrefab.transform as RectTransform).sizeDelta.x * (Screen.width / 2048f); ;
    }

    private void Start()
    {
        scrollSnap.OnPanelCentered.AddListener(PanelCenteredHandler);
        weaponManager.SpawnButtonsForObtainedWeapons();
    }

    private void PanelCenteredHandler(int id, int ii)
    {
        //vehicleManager.selectedVehicleId = id;
        //CheckIfEnoughBalance(id);
    }

    public void Add(int index)
    {
        scrollSnap.Add(panelPrefab.gameObject, index);
    }
    public void AddAtIndex()
    {
        Add(Convert.ToInt32(addInputField.text));
    }
    public void AddToFront()
    {
        Add(0);
    }
    public void AddToBack()
    {
        Add(scrollSnap.NumberOfPanels);
    }

    public void AddCustom(int id)
    {
        //Weapon weapon = weaponContainer.weaponPrefabs[id].GetComponentInChildren<Weapon>();
        panelPrefab.Initialize(weaponManager, id);
        scrollSnap.Add(panelPrefab.gameObject, scrollSnap.NumberOfPanels);
        //CheckIfEnoughBalance(scrollSnap.NumberOfPanels - 1);
    }

    public void Remove(int index)
    {
        if (scrollSnap.NumberOfPanels > 0)
        {
            // Pagination
            //DestroyImmediate(scrollSnap.Pagination.transform.GetChild(scrollSnap.NumberOfPanels - 1).gameObject);
            //scrollSnap.Pagination.transform.position += new Vector3(toggleWidth / 2f, 0, 0);

            // Panel
            //scrollSnap.Remove(index);
            scrollSnap.RemoveFromBack();
        }
    }
    public void RemoveAtIndex()
    {
        Remove(Convert.ToInt32(removeInputField.text));
    }
    public void RemoveFromFront()
    {
        Remove(0);
    }
    public void RemoveFromBack()
    {
        if (scrollSnap.NumberOfPanels > 0)
        {
            Remove(scrollSnap.NumberOfPanels - 1);
        }
        else
        {
            Remove(0);
        }
    }
}
