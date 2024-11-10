using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberGate : MonoBehaviour
{
    [SerializeField] public Transform spawnPosition;
    [SerializeField] public ParticleSystem portalParticleSystem;

    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cross")) // Replace with your layer name
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cross")) // Replace with your layer name
        {
            isOccupied = false;
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
