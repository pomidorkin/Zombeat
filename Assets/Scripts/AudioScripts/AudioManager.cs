using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    //[SerializeField] AudioSource audioSourceMuted;
    [SerializeField] AudioSource audioSourceLoud;
    [SerializeField] AnimationClip animationClip;

    void Start()
    {
        mixer.SetFloat("MyExposedParam", -80.0f);
        if (animationClip != null)
        {
            audioSourceLoud.PlayDelayed(animationClip.length);
        }
        else
        {
            audioSourceLoud.Play();
        }
    }

    public AudioSource GetAudioSource()
    {
        //return audioSourceMuted;
        return audioSourceLoud;
    }
}
