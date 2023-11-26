using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizerManager : MonoBehaviour
{
    [SerializeField] [Range(0, 7)] int band = 0;
    [SerializeField] [Range(0f, 10f)] float animTriggerValue = 1.6f;
    float elapsedTime;
    [SerializeField] float timeLimit = 0.1f;

    public delegate void PeakReachedAction();
    public event PeakReachedAction OnPeakReachedAction;

    // Record beats
    [SerializeField] List<float> beats; float beatCounter;
    [SerializeField] AudioSource audioSource;
    private float clipLength;
    private float clipLengthCounter = 0;

    private void Start()
    {
        clipLength = audioSource.clip.length;
    }

    void Update()
    {
        if (clipLengthCounter < clipLength)
        {
            clipLengthCounter += Time.deltaTime;
            beatCounter += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            //Logic for setting up
            if (elapsedTime >= timeLimit && AudioPeer.bandBuffer[band] > animTriggerValue)
            {
                elapsedTime = 0;
                OnPeakReachedAction();
                beats.Add(beatCounter);
            }
        }
        
    }
    /*void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeLimit && AudioPeer.bandBuffer[band] > animTriggerValue)
        {
            elapsedTime = 0;
            OnPeakReachedAction();
        }
    }*/
}