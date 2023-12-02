using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlacer : MonoBehaviour
{
    public GameObject prefabToPlace; // The prefab to instantiate
    private GameObject prefabmodel;
    [SerializeField] Camera mainCamera;
    int layerMask;

    private bool modelEnabled = false;

    // TODO: Place the weapons only there where they can be placed
    // Assign parent
    // Visual representation of the ray
    // Hologram Shader should be on the weapon
    // Weapon should only start firing when placed (Or when the level starts)
    // Fix weapon position & rotation

    private void Start()
    {
        layerMask = LayerMask.GetMask("WeaponBase");
    }

    private void Update()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits the cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {

            // Get the hit point on the cube
            if (hit.transform.gameObject.tag == "WeaponBase")
            {
                Vector3 hitPoint = hit.point;

                // Get the normal of the surface at the hit point
                Vector3 surfaceNormal = hit.normal;
                if (!modelEnabled)
                {
                    modelEnabled = true;
                    prefabmodel = Instantiate(prefabToPlace, hitPoint, Quaternion.identity);
                }

                prefabmodel.transform.rotation = Quaternion.LookRotation(surfaceNormal);
                prefabmodel.transform.position = hitPoint;
                Debug.Log(hitPoint);
                if (Input.GetMouseButtonDown(0))
                {
                    // Instantiate the prefab at the hit point
                    GameObject prefabInstance = Instantiate(prefabToPlace, hitPoint, Quaternion.identity);

                    // Rotate the prefab to align with the surface normal
                    prefabInstance.transform.rotation = Quaternion.LookRotation(surfaceNormal);
                }
            }
        }
    }


    /* private void Update()
     {
         if (Input.GetMouseButtonDown(0))
         {
             // Cast a ray from the camera to the mouse position
             Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;

             // Check if the ray hits the cube
             if (Physics.Raycast(ray, out hit, Mathf.Infinity))
             {
                 // Get the hit point on the cube
                 Vector3 hitPoint = hit.point;

                 // Get the normal of the surface at the hit point
                 Vector3 surfaceNormal = hit.normal;

                 // Instantiate the prefab at the hit point
                 GameObject prefabInstance = Instantiate(prefabToPlace, hitPoint, Quaternion.identity);

                 // Rotate the prefab to align with the surface normal
                 prefabInstance.transform.rotation = Quaternion.LookRotation(surfaceNormal);
             }
         }
     }*/
}
