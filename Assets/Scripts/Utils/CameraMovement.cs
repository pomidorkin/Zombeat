using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    private Vector3 previousPosition;

    private void Start()
    {
            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);

            Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            camera.transform.position = target.position;

            camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            // "new Vector3()" assigns the point of rotation. It can be swapped for the object of interest.
            camera.transform.Translate(new Vector3(0, 0, -10f));

            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            camera.transform.position = target.position;

            camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            // "new Vector3()" assigns the point of rotation. It can be swapped for the object of interest.
            camera.transform.Translate(new Vector3(0, 0, -10f));

            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
