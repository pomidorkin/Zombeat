using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float destroyTimer = 3.0f;

    private void OnEnable()
    {
        Destroy(gameObject, destroyTimer);
    }
}
