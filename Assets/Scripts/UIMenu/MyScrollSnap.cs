using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class MyScrollSnap : MonoBehaviour
{
    [SerializeField] private VehicleSelectionUnit panelPrefab;
    [SerializeField] private Toggle togglePrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private InputField addInputField, removeInputField;
    [SerializeField] public SimpleScrollSnap scrollSnap;
    [SerializeField] public VehicleManager vehicleManager;
    [SerializeField] private VehicleContainer vehicleContainer;

    private float toggleWidth;

    private void Awake()
    {
        toggleWidth = (togglePrefab.transform as RectTransform).sizeDelta.x * (Screen.width / 2048f); ;
    }

    private void Start()
    {
        scrollSnap.OnPanelCentered.AddListener(PanelCenteredHandler);
    }

    private void PanelCenteredHandler(int id, int ii)
    {
        vehicleManager.selectedVehicleId = id;
    }

    public void Add(int index)
    {
        // Pagination
        /*Toggle toggle = Instantiate(togglePrefab, scrollSnap.Pagination.transform.position + new Vector3(toggleWidth * (scrollSnap.NumberOfPanels + 1), 0, 0), Quaternion.identity, scrollSnap.Pagination.transform);
        toggle.group = toggleGroup;
        scrollSnap.Pagination.transform.position -= new Vector3(toggleWidth / 2f, 0, 0);*/

        // Panel
        //panelPrefab.GetComponent<Image>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        //panelPrefab.vehicleId = Progress.Instance.playerInfo.vehicleSaveDatas[index].id;
        panelPrefab.vehicleId = index;
        panelPrefab.vehicleManager = vehicleManager;
        // TEST
        // Далее нужно будет отображать в прогресс барах здоровье + healthIncrementValue * timesHealthUpgrated
        Vehicle vehicle = vehicleContainer.vehiclePrefabs[index];
        panelPrefab.carImage.sprite = vehicle.carImage;
        panelPrefab.carHealthProgressBar.ChangeValue(vehicle.health);
        panelPrefab.carSpeedProgressBar.ChangeValue(vehicle.speed);
        // END_TEST
        //vehicleContainer.vehiclePrefabs[selectedVehicleId]
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

    public void Remove(int index)
    {
        if (scrollSnap.NumberOfPanels > 0)
        {
            // Pagination
            DestroyImmediate(scrollSnap.Pagination.transform.GetChild(scrollSnap.NumberOfPanels - 1).gameObject);
            scrollSnap.Pagination.transform.position += new Vector3(toggleWidth / 2f, 0, 0);

            // Panel
            scrollSnap.Remove(index);
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