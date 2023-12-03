using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundUnit soundUnit;
    [SerializeField] TweenTest tweenTest;
    [SerializeField] private List<float> beatValues;
    [SerializeField] OrchestraManager orchestraManager;

    private bool playing = false;
    private float timeCounter = 0f;
    private int beatCounter = 0;
    float clipLength;

    // TODO: Weapon & EnemySoundPlayer classes share similar functionality. Refactor these classes to implement an interface.

    private void OnEnable()
    {
        if (orchestraManager == null)
        {
            orchestraManager = FindFirstObjectByType<OrchestraManager>();
        }
        audioSource.clip = soundUnit.GetAudioClip();
        clipLength = soundUnit.GetSoundUnitLength();
        if (!orchestraManager.keySpecified && !soundUnit.isNeutral)
        {
            orchestraManager.keySpecified = true;
            orchestraManager.currentMusicKey = soundUnit.GetSoundUnitKey();
        }
        orchestraManager.OnMusicPlayed += StartPlaying;
        
    }

    void Update()
    {
        if (playing)
        {
            timeCounter += Time.deltaTime;
            if (beatValues.Count > beatCounter && (timeCounter >= beatValues[beatCounter]))
            {
                beatCounter++;
                tweenTest.TriggerTween();
            }
            else if (timeCounter >= clipLength)
            {
                beatCounter = 0;
                timeCounter = 0;
                audioSource.Play();
            }
        }
    }

    private void StartPlaying()
    {
        if (!playing)
        {
            timeCounter = 0;
            beatCounter = 0;
            playing = true;

            audioSource.Play();
        }
    }
}
