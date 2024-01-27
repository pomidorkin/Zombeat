using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRemover : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
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
                //Vector3 hitPoint = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
