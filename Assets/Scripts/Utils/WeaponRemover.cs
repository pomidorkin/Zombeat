using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRemover : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    int layerMask;
    // TODO:
    // ќсвобождать слот тачки при удалении
    // ƒелать оружие снова доступном при удалении
    // ѕровер€ть, последнее ли это оружие оставшеес€ на тачке
    // ≈сли последнее, то обнул€ть тональность и BPM тачки
    // ћожет быть останавливать ќркестор

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
