using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundPlayer : MonoBehaviour
{
    // TODO: This code sould be refactored to display 2-dimentional arrays in the inspector.
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundUnit aSharpMSoundUnits;
    [SerializeField] SoundUnit dSharpMSoundUnits;
    [SerializeField] public OrchestraManager orchestraManager;

    private bool playing = false;
    private float timeCounter = 0f;
    float clipLength;

    private void Start()
    {
        if (orchestraManager.playingAllowed)
        {
            ChooseSuitableTrack();
        }
        orchestraManager.OnMusicPlayed += StartPlaying;
    }

    private void ChooseSuitableTrack()
    {
        if (orchestraManager.chosenVehicle.vehicleMainKey == SoundUnitKey.A_SHARP_M)
        {
            AudioClip[] clips = aSharpMSoundUnits.GetAudioClips();
            int rnd = Random.Range(0, clips.Length);
            audioSource.clip = clips[rnd];
            clipLength = clips[rnd].length;
            timeCounter = 0;
            //audioSource.Play();
        }
        else
        {
            AudioClip[] clips = dSharpMSoundUnits.GetAudioClips();
            int rnd = Random.Range(0, clips.Length);
            audioSource.clip = clips[rnd];
            clipLength = clips[rnd].length;
            timeCounter = 0;
            //audioSource.Play();
        }
    }

    private void OnDisable()
    {
        orchestraManager.OnMusicPlayed -= StartPlaying;
    }
    void Update()
    {
        if (playing)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= clipLength)
            {
                if (orchestraManager.chosenVehicle.vehicleMainKey == SoundUnitKey.A_SHARP_M)
                {
                    AudioClip[] clips = aSharpMSoundUnits.GetAudioClips();
                    int rnd = Random.Range(0, clips.Length);
                    audioSource.clip = clips[rnd];
                    clipLength = clips[rnd].length;
                    timeCounter = 0;
                    audioSource.Play();
                }
                else
                {
                    AudioClip[] clips = dSharpMSoundUnits.GetAudioClips();
                    int rnd = Random.Range(0, clips.Length);
                    audioSource.clip = clips[rnd];
                    clipLength = clips[rnd].length;
                    timeCounter = 0;
                    audioSource.Play();
                }
                
            }
        }
    }

    private void StartPlaying()
    {
        if (!playing)
        {
            ChooseSuitableTrack();
            //timeCounter = 0;
            playing = true;

            audioSource.Play();
        }
    }
}
