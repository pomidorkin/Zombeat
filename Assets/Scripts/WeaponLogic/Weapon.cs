using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] public SoundUnit soundUnit;
    [SerializeField] TweenTest tweenTest;
    [SerializeField] private List<float> beatValues;
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] GameObject weaponBarrel;
    [SerializeField] public Transform parentObject;
    [SerializeField] public SquishStretchTween squishStretchTween;
    [SerializeField] public WeaponOverlay weaponOverlay;
    [SerializeField] public WeaponTracking weaponTracking;
    [SerializeField] public Sprite weaponImage;
    [SerializeField] public string weaponName;
    [SerializeField] public int weaponPrice;

    public WeaponSaveData weaponSaveData;

    public WeaponType weaponType;

    public float weaponDamage;

    public bool isWaveEffector = false;
    private WaveEffector waveEffector;

    private bool playing = false;
    public bool isPlaced = false;
    private float timeCounter = 0f;
    private int beatCounter = 0;
    float clipLength;
    public bool paused = false;

    // Raycast logic
    int enemyLayerMask;

    // TODO: Weapon & EnemySoundPlayer classes share similar functionality. Refactor these classes to implement an interface.
    // Weapon should have a raycast logic where it would call hitBox.OnRayCast() method if hitbox is hit

    private void OnEnable()
    {
        if (orchestraManager == null)
        {
            orchestraManager = FindFirstObjectByType<OrchestraManager>();
        }
        if (isWaveEffector && !orchestraManager.waveEffectorWeapon)
        {
            waveEffector = orchestraManager.waveEffector;
            orchestraManager.waveEffectorWeapon = this;
        }
        audioSource.clip = soundUnit.GetAudioClip();
        audioSource.volume = Progress.Instance.playerInfo.masterSoundVolume * weaponSaveData.weaponSoundVolume;
        orchestraManager.AddWeaponToList(this);
        orchestraManager.OnMusicPlayed += StartPlaying;
        orchestraManager.OnVehicleSet += KeySpecifiedChecker;
        orchestraManager.OnMusicStopped += StopPlaying;
    }

    private void KeySpecifiedChecker()
    {
        if (!orchestraManager.chosenVehicle.keySpecified && !soundUnit.isNeutral)
        {
            orchestraManager.chosenVehicle.keySpecified = true;
            orchestraManager.chosenVehicle.vehicleMainKey = soundUnit.GetSoundUnitKey();
            orchestraManager.chosenVehicle.vehicleMainBPM = soundUnit.GetSoundUnitBPM();
            orchestraManager.chosenVehicle.SaveSoundSettings();
        }
    }

    private void Start()
    {

        /*if (!orchestraManager.chosenVehicle.keySpecified && !soundUnit.isNeutral)
        {
            orchestraManager.chosenVehicle.keySpecified = true;
            orchestraManager.chosenVehicle.vehicleMainKey = soundUnit.GetSoundUnitKey();
            orchestraManager.chosenVehicle.vehicleMainBPM = soundUnit.GetSoundUnitBPM();
            orchestraManager.chosenVehicle.SaveSoundSettings();
        }*/
        clipLength = soundUnit.GetSoundUnitLength();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        weaponTracking.interestedType = weaponSaveData.preferredEnemy;
    }



    private void OnDisable()
    {
        orchestraManager.OnMusicPlayed -= StartPlaying;
        orchestraManager.OnVehicleSet -= KeySpecifiedChecker;
        orchestraManager.OnMusicStopped -= StopPlaying;
    }

    void Update()
    {
        if (playing && isPlaced)
        {
            timeCounter += Time.deltaTime;
            if (beatValues.Count > beatCounter && (timeCounter >= beatValues[beatCounter]))
            {
                beatCounter++;
                tweenTest.TriggerTween();
                if (isWaveEffector && orchestraManager.waveEffectorWeapon == this)
                {
                    waveEffector.TriggerBeatPlayedAction();
                }
                else if (isWaveEffector && !orchestraManager.waveEffectorWeapon)
                {
                    waveEffector = orchestraManager.waveEffector;
                    orchestraManager.waveEffectorWeapon = this;
                    waveEffector.TriggerBeatPlayedAction();
                }

                RaycastShot();
            }
            else if (timeCounter >= clipLength /*&& (clipLength > 0)*/)
            {
                beatCounter = 0;
                timeCounter = 0;
                audioSource.Play();
            }
        }
    }

    private void RaycastShot()
    {
        Ray ray = new Ray(weaponBarrel.transform.position, weaponBarrel.transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        //Debug.Log("Raycast sent");

        // Check if the ray hits the cube
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
        {
            
            HitBox hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                //Debug.Log("Enemy was hit by ray");
                hitBox.OnRaycastHit(this, ray.direction);
            }
        }
    }

    private void StartPlaying()
    {
        if (!playing && isPlaced && !paused)
        {
            timeCounter = 0;
            beatCounter = 0;
            playing = true;

            audioSource.Play();
            audioSource.volume = Progress.Instance.playerInfo.masterSoundVolume * weaponSaveData.weaponSoundVolume;
        }
    }

    private void SlowlySilenceWeapon()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", audioSource.volume, "to", 0.0f, "time", 1.0f, "onupdatetarget", gameObject, "onupdate", "UpdateCounter")); //"easetype", iTween.EaseType.easeInOutQuad
    }

    void UpdateCounter(float newValue)
    {
        audioSource.volume = newValue;
        if (newValue < 0.01f && playing)
        {
            StopPlaying();
        }
    }

    private void StopPlaying()
    {
        playing = false;
        audioSource.Stop();
    }

    public void PausePlaying()
    {
        paused = true;
        //StopPlaying();
        SlowlySilenceWeapon();
        // Overheating icon / element / VFX sheould be activate here
    }

    public void ResumePlaying()
    {
        paused = false;
    }

    public void ChangeVolume(float newVolVal)
    {
        audioSource.volume = Progress.Instance.playerInfo.masterSoundVolume * newVolVal;
    }

    public void SaveChangedVolume(float newVolVal)
    {
        weaponSaveData.weaponSoundVolume = newVolVal;
        Progress.Instance.Save();
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
