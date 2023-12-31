using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffectSubscriber : MonoBehaviour
{
    [SerializeField] WaveEffectorManager waveEffectorManager;
    [SerializeField] ITweenMover tweenMover;
    private WaveEffector waveEffector;

    [SerializeField] float distDelayMultiplier = 0.01f;

    private void Start()
    {
        waveEffector = waveEffectorManager.waveEffector;
        waveEffector.OnBeatPlayed += TriggerWaveEffect;
    }

    private void OnDisable()
    {
        waveEffector.OnBeatPlayed -= TriggerWaveEffect;
    }

    private void TriggerWaveEffect()
    {
        float dist = Vector3.Distance(waveEffector.transform.position, transform.position);
        float delay = dist * distDelayMultiplier;
        Debug.Log("dist: " + dist + ", delay: " + delay);
        StartCoroutine(PlaySoundImpactEffect(delay));
    }

    private IEnumerator PlaySoundImpactEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        tweenMover.MoveChildObject();
    }
}
