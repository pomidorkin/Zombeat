using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target; // The target the camera follows
    public float distance = 5f; // Distance from the target
    public float smoothSpeed = 10f; // How smooth the camera movement is
    public LayerMask collisionMask; // Mask for the layers to consider for collision

    private Vector3 desiredPosition;
    private Vector3 collisionPosition;

    void LateUpdate()
    {
        // Set the desired position
        desiredPosition = target.position - transform.forward * distance;

        // Perform a raycast to detect collision
        RaycastHit hit;
        if (Physics.Raycast(target.position, -transform.forward, out hit, distance, collisionMask))
        {
            collisionPosition = hit.point;
        }
        else
        {
            collisionPosition = desiredPosition;
        }

        // Smoothly move the camera to the collision position
        transform.position = Vector3.Lerp(transform.position, collisionPosition, smoothSpeed * Time.deltaTime);
    }
}
