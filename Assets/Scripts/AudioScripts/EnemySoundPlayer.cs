using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSource;
    [SerializeField] OrchestraManager orchestraManager;


    private bool playing = false;
    private float timeCounter = 0f;
    float clipLength;

    private void OnEnable()
    {

        clipLength = audioSource.clip.length;
        orchestraManager.OnMusicPlayed += StartPlaying;
    }
    void Update()
    {
        if (playing)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= clipLength)
            {
                int rnd = Random.Range(0, audioClips.Length);
                audioSource.clip = audioClips[rnd];
                clipLength = audioClips[rnd].length;
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
            playing = true;

            audioSource.Play();
        }
    }
}
