using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeactivate : MonoBehaviour
{
    private void OnDisable()
    {
        // Destroy the GameObject when it is disabled
        Debug.Log("DestroyOnDeactivate triggered");
        Destroy(gameObject);
    }
}
