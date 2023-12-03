using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlacer : MonoBehaviour
{
    public GameObject prefabToPlace; // The prefab to instantiate
    private GameObject prefabmodel;
    private Weapon childObject;
    [SerializeField] Camera mainCamera;
    int layerMask;

    private bool modelEnabled = false;

    // TODO: Place the weapons only there where they can be placed
    // Hologram Shader should be on the weapon
    // Weapon should only start firing when placed (Or when the level starts)
    // Fix weapon position & rotation (DONE! But improvement is required)
    // Make sure weapons cannot be plased too close to each other
    // Squish & Strech effects when weapon is placed
    // Make weapons follow the target correctly

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
                    childObject = prefabmodel.GetComponentInChildren<Weapon>();
                }

                // Visualizing location for weapon's placement
                prefabmodel.transform.rotation = Quaternion.LookRotation(surfaceNormal);
                // TEST
                Vector3 left = transform.TransformDirection(Vector3.left);
                Vector3 up = transform.TransformDirection(Vector3.up);
                //Vector3 forward = transform.TransformDirection(Vector3.forward);

                Vector3 weaponLeft = prefabmodel.transform.TransformDirection(Vector3.left);
                Vector3 weaponUp = prefabmodel.transform.TransformDirection(Vector3.up);
                //Vector3 weaponForward = prefabmodel.transform.TransformDirection(Vector3.forward);

                // Rotating the weapon depending on it's direction
                // NOTE: This is the test code that does not take into consideration the car's rotation
                if (weaponUp.y > .9f && weaponLeft.z < 0)
                {
                    childObject.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                }
                else if (weaponUp.y > .9f && weaponLeft.z > 0)
                {
                    childObject.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                }
                else if (weaponUp.y < .9f)
                {
                    childObject.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                }

                prefabmodel.transform.eulerAngles = new Vector3(prefabmodel.transform.eulerAngles.x + 90, prefabmodel.transform.eulerAngles.y, prefabmodel.transform.eulerAngles.z);
                prefabmodel.transform.position = hitPoint;

                

                // Placing weapon
                if (Input.GetMouseButtonDown(0))
                {
                    // Instantiate the prefab at the hit point
                    GameObject prefabInstance = Instantiate(prefabToPlace, hitPoint, Quaternion.identity);
                    Weapon prefabInstanceChild = prefabInstance.GetComponentInChildren<Weapon>();

                    // Rotate the prefab to align with the surface normal
                    prefabInstance.transform.rotation = Quaternion.LookRotation(surfaceNormal);

                    if (weaponUp.y > .9f && weaponLeft.z < 0)
                    {
                        prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                    }
                    else if (weaponUp.y > .9f && weaponLeft.z > 0)
                    {
                        prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                    }
                    else if (weaponUp.y < .9f)
                    {
                        prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                    }

                    prefabInstance.transform.eulerAngles = new Vector3(prefabInstance.transform.eulerAngles.x + 90, prefabInstance.transform.eulerAngles.y, prefabInstance.transform.eulerAngles.z);
                    prefabInstance.transform.parent = hit.transform;
                }
            }
        }
    }
}
