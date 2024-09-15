using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffectSubscriber : MonoBehaviour
{
    //[SerializeField] WaveEffectorManager waveEffectorManager;
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] ITweenMover tweenMover;
    private WaveEffector waveEffector;

    [SerializeField] float distDelayMultiplier = 0.01f;

    private void Start()
    {
        waveEffector = orchestraManager.waveEffector;
        waveEffector.OnBeatPlayed += TriggerWaveEffect;
    }

    private void OnDisable()
    {
        waveEffector.OnBeatPlayed -= TriggerWaveEffect;
    }

    private void TriggerWaveEffect()
    {
        float dist = Vector3.Distance(orchestraManager.chosenVehicle.transform.position, transform.position);
        float delay = dist * distDelayMultiplier;
        StartCoroutine(PlaySoundImpactEffect(delay));
    }

    private IEnumerator PlaySoundImpactEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        tweenMover.MoveChildObject();
    }
}
