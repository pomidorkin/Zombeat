using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubcriber : MonoBehaviour
{
    [SerializeField] AudioVisualizerManager visualizerManager;
    //[SerializeField] TweenTest tweenTest;
    [SerializeField] public List<float> beats;

    private void OnEnable()
    {
        visualizerManager.OnPeakReachedAction += Handler;
    }

    private void OnDisable()
    {
        visualizerManager.OnPeakReachedAction -= Handler;
    }

    private void Handler()
    {
        //tweenTest.TriggerTween();
    }
}
