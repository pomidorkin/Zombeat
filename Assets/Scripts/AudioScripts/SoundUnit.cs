using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUnit : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioClip[] clips;
    [SerializeField] SoundUnitKey musicKey;
    private float clipLength;
    [SerializeField] public bool isNeutral = false;

    void Awake()
    {
        if (clip != null)
        {
            clipLength = clip.length;
        }
    }

    public float GetSoundUnitLength()
    {
        return clipLength;
    }

    public AudioClip GetAudioClip()
    {
        return clip;
    }

    public SoundUnitKey GetSoundUnitKey()
    {
        return musicKey;
    }

    public AudioClip[] GetAudioClips()
    {
        return clips;
    }
}
