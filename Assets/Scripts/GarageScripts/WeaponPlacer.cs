using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlacer : MonoBehaviour
{
    //public GameObject prefabToPlace; // The prefab to instantiate
    public WeaponContainer weaponContainer;
    private GameObject prefabmodel;
    private Weapon childObject;
    [SerializeField] public Camera mainCamera;
    int layerMask;
    [SerializeField] public Vehicle vehicle;
    public VehicleWeaponInitializer vehicleWeaponInitializer;

    private bool modelEnabled = false;

    // TODO: Place the weapons only there where they can be placed
    // Hologram Shader should be on the weapon
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
                        prefabmodel = Instantiate(weaponContainer.weaponPrefabs[0], hitPoint, Quaternion.identity); // 0 should be replaced for the chosen weapon id
                    childObject = prefabmodel.GetComponentInChildren<Weapon>();
                        if (CheckAvailableSlot())
                        {
                            // Make weapon shader red, because the weapon cannot be placed;
                            Debug.Log("Red shader;");
                        }
                    }

                    // Visualizing location for weapon's placement
                    prefabmodel.transform.rotation = Quaternion.LookRotation(surfaceNormal);

                    prefabmodel.transform.eulerAngles = new Vector3(prefabmodel.transform.eulerAngles.x + 90, prefabmodel.transform.eulerAngles.y, prefabmodel.transform.eulerAngles.z);
                    prefabmodel.transform.position = hitPoint;

                    Vector3 testForward = transform.TransformDirection(Vector3.left);
                    Vector3 prefabLeft = prefabmodel.transform.TransformDirection(Vector3.left);
                    Vector3 prefabUp = prefabmodel.transform.TransformDirection(Vector3.up);

                    float forwardLeftMagnitude = (testForward + prefabLeft).magnitude;
                    float upForwardMagnitude = (prefabUp + testForward).magnitude;

                    if (forwardLeftMagnitude > 1.3f && upForwardMagnitude < 0.5f)
                    {
                        childObject.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                    }
                    else if (forwardLeftMagnitude < 0.5f && upForwardMagnitude > 1.2f)
                    {
                        childObject.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                    }
                    else if (forwardLeftMagnitude > 1.3f && upForwardMagnitude > 1.6f)
                    {
                        childObject.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                    }





                    // Placing weapon
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (CheckAvailableSlot())
                        {
                            // Instantiate the prefab at the hit point
                            GameObject prefabInstance = Instantiate(weaponContainer.weaponPrefabs[0], hitPoint, Quaternion.identity); // 0 should be replaced for the chosen weapon id
                        Weapon prefabInstanceChild = prefabInstance.GetComponentInChildren<Weapon>();
                        prefabInstanceChild.weaponSaveData = Progress.Instance.playerInfo.weaponSaveDatas[0]; // 0 should be replaced for the chosen weapon id

                            // Rotate the prefab to align with the surface normal
                            prefabInstance.transform.rotation = Quaternion.LookRotation(surfaceNormal);

                            if (forwardLeftMagnitude > 1.3f && upForwardMagnitude < 0.5f)
                            {
                                prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                            prefabInstanceChild.weaponSaveData.childRotationY = -90f;
                            }
                            else if (forwardLeftMagnitude < 0.5f && upForwardMagnitude > 1.2f)
                            {
                                prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                            prefabInstanceChild.weaponSaveData.childRotationY = 180f;
                        }
                            else if (forwardLeftMagnitude > 1.3f && upForwardMagnitude > 1.6f)
                            {
                                prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                            prefabInstanceChild.weaponSaveData.childRotationY = 90f;
                        }

                            prefabInstance.transform.eulerAngles = new Vector3(prefabInstance.transform.eulerAngles.x + 90, prefabInstance.transform.eulerAngles.y, prefabInstance.transform.eulerAngles.z);
                            prefabInstance.transform.parent = hit.transform;
                            prefabInstanceChild.isPlaced = true;


                        // TEST_SAVING
                        prefabInstanceChild.weaponSaveData.position = prefabInstanceChild.parentObject.localPosition;
                        prefabInstanceChild.weaponSaveData.rotation = prefabInstanceChild.parentObject.localRotation;
                        prefabInstanceChild.weaponSaveData.idVehicle = vehicleWeaponInitializer.selectedVehicleId;
                        prefabInstanceChild.weaponSaveData.placed = true;
                        //Progress.Instance.playerInfo.weaponSaveDatas.Add(prefabInstanceChild.weaponSaveData);
                        Progress.Instance.Save();
                        // END_TEST



                            // Occupy Slot
                        OccupySlot();
                        }
                        else
                        {
                            Debug.Log("There is no slot for this weapon!");
                        }
                    }
                }
            }
    }

    private bool CheckAvailableSlot()
    {
        bool slotAvailable = false;
        foreach (WeaponSlot weaponSlot in vehicle.weaponSlots)
        {
            if (!weaponSlot.occupied && weaponSlot.weaponTypeSlot == childObject.weaponType)
            {
                slotAvailable = true;
                break;
            }
        }

        return slotAvailable;
    }

    private void OccupySlot()
    {
        foreach (WeaponSlot weaponSlot in vehicle.weaponSlots)
        {
            if (!weaponSlot.occupied && weaponSlot.weaponTypeSlot == childObject.weaponType)
            {
                weaponSlot.occupied = true;
                break;
            }
        }
    }
}
