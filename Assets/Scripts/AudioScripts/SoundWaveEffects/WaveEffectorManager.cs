using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffectorManager : MonoBehaviour
{
    public WaveEffector waveEffector;

    private void OnEnable()
    {
        waveEffector = FindFirstObjectByType<WaveEffector>();
    }

    /*private void OnEnable()
    {
        waveEffector = FindFirstObjectByType<WaveEffector>();
    }*/
}
