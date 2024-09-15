using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{/*
    public Transform target; // The target the missile will home in on
    public float speed = 10f; // Speed of the missile
    public float turnSpeed = 5f; // How quickly the missile turns towards the target
    public float destroyDistance = 1f; // Distance at which the missile will destroy itself
    public float updateInterval = 0.1f; // How often to recalculate the direction in seconds
    public float straightTime = 5f; // Time in seconds to fly straight before homing in

    private float timeSinceLaunch;
    private float timeSinceLastUpdate;
    private Vector3 directionToTarget;
    private bool isHoming;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] GameObject missileObject;

    private void Start()
    {
        // Initialize direction to forward direction
        directionToTarget = transform.forward;
    }

    private void Update()
    {
        timeSinceLaunch += Time.deltaTime;
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLaunch >= straightTime)
        {
            isHoming = true;

            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0f;

                // Update direction to target
                if (target != null)
                {
                    directionToTarget = (target.position - transform.position).normalized;
                }
            }
        }

        // Rotate the missile towards the target if it is homing
        if (isHoming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Move the missile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Check if the missile is close enough to the target to destroy itself
        if (Vector3.Distance(transform.position, target.position) <= destroyDistance)
        {
            // Destroy the missile (you might want to instantiate an explosion effect here)
            StartCoroutine(DestroySelf());
            missileObject.SetActive(false);
            explosionParticle.Play();
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }*/

    public Transform target; // The target the missile will home in on
    public float initialSpeed = 2f; // Speed of the missile during the initial slow phase
    public float finalSpeed = 10f; // Speed of the missile once it accelerates
    public float accelerationTime = 5f; // Time in seconds to accelerate from initialSpeed to finalSpeed
    public float initialPhaseDuration = 3f; // Duration in seconds to maintain the initial slow speed
    public float turnSpeed = 5f; // How quickly the missile turns towards the target
    public float destroyDistance = 1f; // Distance at which the missile will destroy itself
    public float updateInterval = 0.1f; // How often to recalculate the direction in seconds
    public float straightTime = 5f; // Time in seconds to fly straight before homing in

    private float timeSinceLaunch;
    private float timeSinceLastUpdate;
    private Vector3 directionToTarget;
    private bool isHoming;
    private float currentSpeed;
    private float accelerationProgress; // Track the progress of acceleration
    private bool isAccelerating; // Flag to determine if acceleration has started

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private GameObject missileObject;
    [SerializeField] private GameObject[] exhaustFXs;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        // Initialize direction to forward direction and set the initial speed
        directionToTarget = transform.forward;
        currentSpeed = initialSpeed;
        accelerationProgress = 0f;
        isAccelerating = false;
        //sfx[0].Play();
    }

    private void Update()
    {
        timeSinceLaunch += Time.deltaTime;
        timeSinceLastUpdate += Time.deltaTime;

        // Start acceleration after the initial phase duration
        if (timeSinceLaunch > initialPhaseDuration && !isAccelerating)
        {
            isAccelerating = true;
            exhaustFXs[0].SetActive(true);
            exhaustFXs[1].SetActive(true);
            audioSource.clip = audioClips[1];
            audioSource.loop = true;
            audioSource.Play();
        }

        if (isAccelerating)
        {
            // Update acceleration progress
            if (accelerationProgress < 1f)
            {
                accelerationProgress += Time.deltaTime / accelerationTime;
                accelerationProgress = Mathf.Clamp01(accelerationProgress);
                currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, accelerationProgress);
            }
            else
            {
                // Ensure speed does not exceed final speed
                currentSpeed = finalSpeed;
            }
        }

        // Update direction to target if homing
        if (timeSinceLaunch > straightTime)
        {
            isHoming = true;

            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0f;

                // Update direction to target
                if (target != null)
                {
                    directionToTarget = (target.position - transform.position).normalized;
                }
            }
        }

        // Rotate the missile towards the target if it is homing
        if (isHoming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Move the missile forward
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);

        // Check if the missile is close enough to the target to destroy itself
        if (target != null && Vector3.Distance(transform.position, target.position) <= destroyDistance)
        {
            // Destroy the missile (instantiate an explosion effect here)
            StartCoroutine(DestroySelf());
            if (explosionParticle != null) explosionParticle.Play();
            if (missileObject != null) missileObject.SetActive(false);
            exhaustFXs[0].SetActive(false);
            exhaustFXs[1].SetActive(false);
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
