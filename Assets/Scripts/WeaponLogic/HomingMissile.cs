using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
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
    }
}
