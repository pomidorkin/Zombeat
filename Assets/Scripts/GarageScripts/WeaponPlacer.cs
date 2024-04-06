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
    private WeaponManager weaponManager;
    [SerializeField] bool reverseTopPosition = false;

    private Vector3 vehicleForward;
    private Vector3 vehicleUp;

    public bool previewWeaponCanBePlaced = false;
    private bool modelEnabled = false;

    // TODO: Place the weapons only there where they can be placed
    // Hologram Shader should be on the weapon
    // Make sure weapons cannot be plased too close to each other
    // Squish & Strech effects when weapon is placed
    // Make weapons follow the target correctly

    private void Start()
    {
        layerMask = LayerMask.GetMask("WeaponBase");
        weaponManager = vehicleWeaponInitializer.weaponManager;
        weaponManager.currentWeaponPlacer = this;
        vehicleForward = transform.TransformDirection(Vector3.left);
        vehicleUp = transform.TransformDirection(Vector3.up);
    }

    private void Update()
    {
        if (previewWeaponCanBePlaced)
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
                        prefabmodel = Instantiate(weaponContainer.weaponPrefabs[vehicleWeaponInitializer.weaponManager.selectedWeaponId], hitPoint, Quaternion.identity); // 0 should be replaced for the chosen weapon id
                        childObject = prefabmodel.GetComponentInChildren<Weapon>();
                        childObject.weaponOverlay.SetColorToAllowed();
                        childObject.weaponOverlay.isBeingPlaced = true;
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

                    //Vector3 vehicleForward = transform.TransformDirection(Vector3.left);
                    //Vector3 vehicleUp = transform.TransformDirection(Vector3.up);
                    Vector3 prefabLeft = prefabmodel.transform.TransformDirection(Vector3.left); // Weapon
                    Vector3 prefabUp = prefabmodel.transform.TransformDirection(Vector3.up); // Weapon
                    //Debug.Log("vehicleUp (car): " + vehicleUp);
                    //Debug.Log("prefabUp (weapon): " + prefabUp);
                    /*Debug.Log("forwardLeftMagnitude: " + (vehicleForward + prefabLeft).magnitude);
                    Debug.Log("upForwardMagnitude: " + (prefabUp + vehicleForward).magnitude);
                    Debug.Log("Magnitude (both): " + (vehicleUp + prefabUp).magnitude);*/

                    float forwardLeftMagnitude = (vehicleForward + prefabLeft).magnitude;
                    float upForwardMagnitude = (prefabUp + vehicleForward).magnitude;
                    float upMagnitude = (vehicleUp + prefabUp).magnitude;


                    if (forwardLeftMagnitude > 1.3f && upForwardMagnitude < 0.5f && upMagnitude < 1.9f)
                    {
                        //childObject.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                        childObject.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                        //prefabmodel.transform.eulerAngles = new Vector3(prefabmodel.transform.eulerAngles.x/* - 90*/, prefabmodel.transform.eulerAngles.y - 90, prefabmodel.transform.eulerAngles.z - 90);
                    }
                    else if (/*forwardLeftMagnitude < 0.5f && upForwardMagnitude > 1.2f*/ upMagnitude >= 1.9f)
                    {
                        //childObject.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                        if (!reverseTopPosition)
                        {
                            childObject.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                        }
                        else
                        {

                            childObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    else if (forwardLeftMagnitude > 1.3f && upForwardMagnitude > 1.6f && upMagnitude < 1.9f)
                    {
                        //childObject.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                        childObject.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                    }





                    // Placing weapon
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (CheckAvailableSlot() && !childObject.weaponOverlay.isOverlaying)
                        {
                            // Instantiate the prefab at the hit point
                            GameObject prefabInstance = Instantiate(weaponContainer.weaponPrefabs[vehicleWeaponInitializer.weaponManager.selectedWeaponId], hitPoint, Quaternion.identity); // 0 should be replaced for the chosen weapon id
                            Weapon prefabInstanceChild = prefabInstance.GetComponentInChildren<Weapon>();
                            prefabInstanceChild.weaponSaveData = Progress.Instance.playerInfo.weaponSaveDatas[vehicleWeaponInitializer.weaponManager.selectedWeaponId]; // 0 should be replaced for the chosen weapon id

                            // Rotate the prefab to align with the surface normal
                            prefabInstance.transform.rotation = Quaternion.LookRotation(surfaceNormal);

                            if (forwardLeftMagnitude > 1.3f && upForwardMagnitude < 0.5f && upMagnitude < 1.9f)
                            {
                                /*prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                                prefabInstanceChild.weaponSaveData.childRotationY = -90f;*/
                                prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                                prefabInstanceChild.weaponSaveData.childRotationY = 90f;
                                //prefabInstance.transform.eulerAngles = new Vector3(prefabmodel.transform.eulerAngles.x/* - 90*/, prefabmodel.transform.eulerAngles.y/* - 90*/, prefabmodel.transform.eulerAngles.z/* - 90*/);
                            }
                            else if (/*forwardLeftMagnitude < 0.5f && upForwardMagnitude > 1.2f*/ upMagnitude >= 1.9f)
                            {
                                if (!reverseTopPosition)
                                {
                                    prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                                    prefabInstanceChild.weaponSaveData.childRotationY = 180f;
                                }
                                else
                                {
                                    prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                    prefabInstanceChild.weaponSaveData.childRotationY = 0;
                                }
                                //prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                                //prefabInstanceChild.weaponSaveData.childRotationY = 180f;
                                /*prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                prefabInstanceChild.weaponSaveData.childRotationY = 0;*/
                            }
                            else if (forwardLeftMagnitude > 1.3f && upForwardMagnitude > 1.6f && upMagnitude < 1.9f)
                            {
                                /*prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, 90f, 0);
                                prefabInstanceChild.weaponSaveData.childRotationY = 90f;*/
                                prefabInstanceChild.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                                prefabInstanceChild.weaponSaveData.childRotationY = -90f;
                            }

                            prefabInstance.transform.eulerAngles = new Vector3(prefabInstance.transform.eulerAngles.x + 90, prefabInstance.transform.eulerAngles.y, prefabInstance.transform.eulerAngles.z);
                            prefabInstance.transform.parent = hit.transform;
                            //prefabInstance.transform.SetParent(hit.transform.GetComponent<Vehicle>().weaponHolderParent.transform);
                            prefabInstanceChild.isPlaced = true;


                            // SAVING
                            prefabInstance.transform.SetParent(/*hit.transform.GetComponent<Vehicle>().weaponHolderParent.transform*/vehicle.weaponHolderParent.transform);
                            Debug.Log("Weapon localPosition: " + prefabInstanceChild.parentObject.localPosition);
                            prefabInstanceChild.weaponSaveData.position = prefabInstanceChild.parentObject.localPosition;
                            prefabInstanceChild.weaponSaveData.rotation = prefabInstanceChild.parentObject.localRotation;
                            prefabInstanceChild.weaponSaveData.idVehicle = vehicleWeaponInitializer.selectedVehicleId;
                            prefabInstanceChild.weaponSaveData.placed = true;
                            prefabInstanceChild.squishStretchTween.TriggerTween();
                            //Progress.Instance.playerInfo.weaponSaveDatas.Add(prefabInstanceChild.weaponSaveData);

                            //prefabInstance.transform.SetParent(/*hit.transform.GetComponent<Vehicle>().weaponHolderParent.transform*/vehicle.weaponHolderParent.transform);





                            // Occupy Slot
                            OccupySlot(prefabInstanceChild);

                            previewWeaponCanBePlaced = false;
                            Destroy(prefabmodel);
                            modelEnabled = false;

                            weaponManager.weaponRemover.enabled = true;
                            weaponManager.SpawnButtonsForObtainedWeapons();
                        }
                        else
                        {
                            Debug.Log("There is no slot for this weapon!");
                        }
                    }
                }
            }
        }
    }

    public void ChangePrefabModel()
    {
        if (modelEnabled)
        {
            Destroy(prefabmodel);
            modelEnabled = false;
            childObject = null;
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

    private void OccupySlot(Weapon weapon)
    {
        foreach (WeaponSlot weaponSlot in vehicle.weaponSlots)
        {
            if (!weaponSlot.occupied && weaponSlot.weaponTypeSlot == childObject.weaponType)
            {
                weaponSlot.occupied = true;
                break;
            }
        }

        if (!vehicle.keySpecified)
        {
            vehicle.keySpecified = true;
            vehicle.vehicleMainBPM = weapon.soundUnit.GetSoundUnitBPM();
            vehicle.vehicleMainKey = weapon.soundUnit.GetSoundUnitKey();
            vehicleWeaponInitializer.orchestraManager.mainBPM = weapon.soundUnit.GetSoundUnitBPM();
            if (!vehicleWeaponInitializer.orchestraManager.playingAllowed)
            {
                vehicleWeaponInitializer.orchestraManager.playingAllowed = true;
            }
            vehicleWeaponInitializer.orchestraManager.ResetTriggerValue();
        }

        vehicle.SaveSoundSettings();
        //Progress.Instance.Save();
    }
}
