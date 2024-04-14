using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponTracking : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private Rig weaponRig;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] float radius = 10f;
    [SerializeField] float retargetSpeed = 5f;
    //[SerializeField] private Camera camera;
    //private List<PointOfInterest> POIs;
    private List<Enemy> interestedPOIs;
    private List<Enemy> allPOIs;
    float radiusSqr;

    // Finding Enemies variables
    [SerializeField] public EnemyType interestedType;
    [SerializeField] AllEnemiesManager allEnemiesManager;

    Transform tracking;
    bool targetFound = false;

    /*TODO:
    Each Point of interest should add itself to the list of POIs when it's initialized;
    Create a system where a different can of enemy can have a higher priority that the others
    Now PointOfInterest is an empty script. Maybe it's better to use tags instead?
    e.g.: GameObject.FindGameObjectsWithTag("Enemy");
     */


    private void Awake()
    {
        if (!allEnemiesManager)
        {
            allEnemiesManager = FindFirstObjectByType<AllEnemiesManager>();
        }
        allEnemiesManager.OnListUpdated += OnListUpdatedHandler;
    }

    private void OnDisable()
    {
        allEnemiesManager.OnListUpdated -= OnListUpdatedHandler;
    }

    private void OnListUpdatedHandler()
    {
        interestedPOIs = allEnemiesManager.GetInterestedEnemies(interestedType);
        allPOIs = allEnemiesManager.GetInterestedEnemies(EnemyType.Default);
    }

    void Start()
    {
        interestedPOIs = allEnemiesManager.GetInterestedEnemies(interestedType);
        allPOIs = allEnemiesManager.GetInterestedEnemies(EnemyType.Default);
        //new List<PointOfInterest>(FindObjectsOfType<PointOfInterest>());
        radiusSqr = radius * radius;
    }

    void Update()
    {
        tracking = null;
        targetFound = false;
        foreach (Enemy poi in interestedPOIs)
        {
            Vector3 delta = poi.transform.position - transform.position;
            if (delta.sqrMagnitude < radiusSqr)
            {
                float angle = Vector3.Angle(transform.forward, delta);
                if (angle < maxAngle)
                {
                    targetFound = true;
                    tracking = poi.transform;
                    break;
                }
            }
        }

        if (!targetFound)
        {
            foreach (Enemy poi in allPOIs)
            {
                Vector3 delta = poi.transform.position - transform.position;
                if (delta.sqrMagnitude < radiusSqr)
                {
                    float angle = Vector3.Angle(transform.forward, delta);
                    if (angle < maxAngle)
                    {
                        targetFound = true;
                        tracking = poi.transform;
                        break;
                    }
                }
            }
        }

        float rigWeight = 0f;

        Vector3 targetPos = transform.position + (transform.forward * 2f);

        if (tracking != null)
        {
            targetPos = tracking.position;
            rigWeight = 1f;
        }
        /*else
        {
            float angle = Vector3.Angle(transform.forward, camera.transform.forward);
            if (angle < maxAngle)
            {
                targetPos = transform.position + camera.transform.forward;
                rigWeight = 1f;
            }
        }*/
        target.position = Vector3.Lerp(target.position, targetPos, Time.deltaTime * retargetSpeed);
        weaponRig.weight = Mathf.Lerp(weaponRig.weight, rigWeight, Time.deltaTime * 2f);
    }
}
