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

    public float weaponDamage;

    private bool isWaveEffector = false;
    private WaveEffector waveEffector;

    private bool playing = false;
    private float timeCounter = 0f;
    private int beatCounter = 0;
    float clipLength;

    // TODO: Weapon & EnemySoundPlayer classes share similar functionality. Refactor these classes to implement an interface.
    // Weapon should have a raycast logic where it would call hitBox.OnRayCast() method if hitbox is hit

    private void OnEnable()
    {
        if (orchestraManager == null)
        {
            orchestraManager = FindFirstObjectByType<OrchestraManager>();
        }
        audioSource.clip = soundUnit.GetAudioClip();
        if (!orchestraManager.keySpecified && !soundUnit.isNeutral)
        {
            orchestraManager.keySpecified = true;
            orchestraManager.currentMusicKey = soundUnit.GetSoundUnitKey();
        }
        orchestraManager.OnMusicPlayed += StartPlaying;
        
    }

    private void Start()
    {

        clipLength = soundUnit.GetSoundUnitLength();
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
            if (beatValues.Count > beatCounter && (timeCounter >= beatValues[beatCounter]))
            {
                beatCounter++;
                tweenTest.TriggerTween();
                if (isWaveEffector)
                {
                    waveEffector.TriggerBeatPlayedAction();
                }
            }
            else if (timeCounter >= clipLength /*&& (clipLength > 0)*/)
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

    public List<float> GetBeatValues()
    {
        return beatValues;
    }

    public void SetWaveEffector(WaveEffector waveEffector)
    {
        isWaveEffector = true;
        this.waveEffector = waveEffector;
    }
}
