using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [Header("Rotation Settings")]
    public RotationAxis rotationAxis = RotationAxis.Y;
    public bool isNegative = false;
    public float rotationSpeed = 50f;

    void Update()
    {
        // Determine the direction of rotation
        float direction = isNegative ? -1f : 1f;
        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        // Rotate the object based on the selected axis
        switch (rotationAxis)
        {
            case RotationAxis.X:
                transform.Rotate(Vector3.right * rotationAmount);
                break;
            case RotationAxis.Y:
                transform.Rotate(Vector3.up * rotationAmount);
                break;
            case RotationAxis.Z:
                transform.Rotate(Vector3.forward * rotationAmount);
                break;
        }
    }
}
