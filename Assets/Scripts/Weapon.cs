using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TweenTest tweenTest;
    [SerializeField] private List<float> beatValues;
    private float timeCounter = 0f;
    private int beatCounter = 0;
    float clipLength;
    // Start is called before the first frame update
    void Start()
    {
        clipLength = audioSource.clip.length;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (beatValues.Count > beatCounter && (timeCounter >= beatValues[beatCounter]))
        {
            beatCounter++;
            tweenTest.TriggerTween();
        }
        else if(timeCounter >= clipLength)
        {
            beatCounter = 0;
            timeCounter = 0;
            audioSource.Play();
        }

    }
}
