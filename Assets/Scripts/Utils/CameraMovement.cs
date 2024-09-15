 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10f; // Default distance from the target
    [SerializeField] private float zoomSpeed = 5f; // Speed at which zooming happens
    [SerializeField] private float minDistance = 2f; // Minimum zoom distance
    [SerializeField] private float maxDistance = 50f; // Maximum zoom distance
    [SerializeField] private float smoothSpeed = 10f; // How smooth the camera movement is
    [SerializeField] private LayerMask collisionMask; // Mask for the layers to consider for collision
    [SerializeField] private float minHeight = 2f; // Minimum height of the camera from the ground

    private Vector3 previousPosition;
    private float currentVerticalAngle = 0f;
    private float currentHorizontalAngle = 0f;

    private void Start()
    {
        previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        UpdateCameraPositionAndRotation();
    }

    private void Update()
    {
        HandleMouseInput();
        HandleCameraCollision();
    }

    private void HandleMouseInput()
    {
        // Handle mouse input for camera control
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            UpdateCameraPositionAndRotation(direction);

            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        // Handle zoom with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Clamp distance to prevent extreme values

        // Always update camera position to reflect the new distance
        UpdateCameraPositionAndRotation();
    }

    private void UpdateCameraPositionAndRotation(Vector3? direction = null)
    {
        if (direction == null)
        {
            direction = Vector3.zero;
        }

        // Calculate desired vertical and horizontal angles
        float verticalInput = ((Vector3)direction).y * 180;
        float horizontalInput = -((Vector3)direction).x * 180;

        // Check if the camera is at or below the minimum height
        Vector3 currentCameraPosition = camera.transform.position;
        bool isAtMinHeight = currentCameraPosition.y <= minHeight;

        // Update angles if the camera is not at the minimum height
        if (!isAtMinHeight || verticalInput >= 0)
        {
            currentVerticalAngle += verticalInput;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -80f, 80f);
        }

        // Update horizontal angle
        currentHorizontalAngle += horizontalInput;

        // Apply rotation to the camera
        camera.transform.position = target.position;
        camera.transform.rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0);

        // Calculate camera position based on distance
        camera.transform.Translate(new Vector3(0, 0, -distance));

        // Ensure the camera stays above the minimum height
        Vector3 newPosition = camera.transform.position;
        newPosition.y = Mathf.Max(newPosition.y, minHeight);
        camera.transform.position = newPosition;
    }

    private void HandleCameraCollision()
    {
        Vector3 desiredPosition = camera.transform.position;
        Vector3 direction = (camera.transform.position - target.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(target.position, direction, out hit, distance, collisionMask))
        {
            // Adjust the camera position to avoid clipping
            camera.transform.position = hit.point;
        }
        else
        {
            // Smoothly move the camera to the desired position
            camera.transform.position = Vector3.Lerp(camera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        // Ensure the camera stays above the minimum height after collision handling
        Vector3 currentPosition = camera.transform.position;
        currentPosition.y = Mathf.Max(currentPosition.y, minHeight);
        camera.transform.position = currentPosition;
    }
}
