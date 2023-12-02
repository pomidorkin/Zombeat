using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchestraManager : MonoBehaviour
{
    public delegate void PlayMusicAction();
    public event PlayMusicAction OnMusicPlayed;

    [SerializeField] int bpm = 126;
    [SerializeField] int numberOfTacts = 8; // How many tacts you wait before you start playing
    private float counter = 0;
    private float triggerValue;

    private void OnEnable()
    {
        triggerValue = ((float)(60f / bpm) * numberOfTacts);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= triggerValue)
        {
            counter = 0;
            OnMusicPlayed();
        }
    }
}