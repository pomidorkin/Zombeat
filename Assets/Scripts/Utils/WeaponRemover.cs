using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRemover : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] VehicleWeaponInitializer vehicleWeaponInitializer;
    private Weapon weapon;
    int layerMask;
    // TODO:
    // ����������� ���� ����� ��� ��������
    // ������ ������ ����� ��������� ��� ��������
    // ���������, ��������� �� ��� ������ ���������� �� �����
    // ���� ���������, �� �������� ����������� � BPM �����
    // ����� ���� ������������� ��������

    private void Start()
    {
        layerMask = LayerMask.GetMask("Weapon");
    }

    void Update()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits the cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.tag == "Weapon")
            {
                weapon = hit.transform.gameObject.GetComponentInChildren<Weapon>();
                EmptyVehicleSlot(weapon, vehicleWeaponInitializer.vehicle);
                //Vector3 hitPoint = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    private void EmptyVehicleSlot(Weapon weapon, Vehicle vehicle)
    {
        foreach (WeaponSlot slot in vehicle.weaponSlots)
        {
            if (slot.weaponTypeSlot == weapon.weaponType && slot.occupied)
            {
                slot.occupied = false;
                weapon.isPlaced = false;
                weapon.weaponSaveData.placed = false;
                //Progress.Instance.playerInfo.weaponSaveDatas[0].placed = false;
                break;
            }
        }
        Progress.Instance.Save();
    }
}
