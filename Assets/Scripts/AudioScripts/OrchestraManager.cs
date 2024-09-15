using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundUnitKey
{
    A_SHARP_M,
    D_SHARP_M
}

public class OrchestraManager : MonoBehaviour
{
    public delegate void PlayMusicAction();
    public event PlayMusicAction OnMusicPlayed;

    public delegate void StopMusicAction();
    public event StopMusicAction OnMusicStopped;

    public delegate void VehicleSetAction();
    public event VehicleSetAction OnVehicleSet;

    public delegate void CulminationAction();
    public event CulminationAction OnCulminationPlayed;

    private int oldBPM;
    public int mainBPM;
    [SerializeField] int numberOfTacts = 8; // How many tacts you wait before you start playing
    [SerializeField] public Vehicle chosenVehicle;
    [SerializeField] public AllEnemiesManager allEnemiesManager;
    public WaveEffector waveEffector;
    public Weapon waveEffectorWeapon;
    //public SoundUnitKey currentMusicKey;
    //public bool keySpecified = false;
    private float counter = 0;
    private float triggerValue;
    public bool playingAllowed = false;

    // Weapon Containers
    public List<Weapon> turretWeapons;
    public List<Weapon> cannonWeapons;
    public List<Weapon> supportWeapons;

    private int tactCounter = 0;
    private int maxNaumberOfTacts = 17;

    [SerializeField] private bool overheatingIsAllowed = true;
    [SerializeField] private bool culminationAllowed = false;
    private bool culminationCanBePlayed = false;

    private void OnEnable()
    {
        CalculateTriggerValue();
    }

    private void Update()
    {
        if (chosenVehicle != null && playingAllowed)
        {
            counter += Time.deltaTime;
            if (counter >= triggerValue)
            {
                counter = 0;
                TactManager();
                OnMusicPlayed();
            }
        }
    }

    private void TactManager()
    {
        if (overheatingIsAllowed)
        {
            if ((tactCounter + 1) < maxNaumberOfTacts)
            {
                tactCounter++;
            }
            else
            {
                tactCounter = 0;
            }
        }
        // Здесь пишется рисунок меллодии, какие треки отключаем, какие нет. В принципе щас окэй, но можно покруче сделать
        List<Weapon> weaponsToSilence = new List<Weapon>();
        switch (tactCounter)
        {
            case 0:
                if (turretWeapons.Count > 0)
                {
                    ResumePlayingForPaused();

                    if (culminationAllowed && culminationCanBePlayed)
                    {
                        OnCulminationPlayed();
                    }
                }
                break;
            case 3:
                if (turretWeapons.Count > 0)
                {
                    ResumePlayingForPaused();
                    weaponsToSilence.AddRange(turretWeapons);
                    weaponsToSilence.AddRange(supportWeapons);
                    SilenceWeapons(weaponsToSilence);
                    culminationCanBePlayed = true;
                }
                break;
            case 5:
                ResumePlayingForPaused();
                break;
            case 7:
                ResumePlayingForPaused();
                weaponsToSilence.AddRange(cannonWeapons);
                weaponsToSilence.AddRange(supportWeapons);
                SilenceWeapons(weaponsToSilence);
                break;
            case 9:
                ResumePlayingForPaused();
                break;
            case 11:
                ResumePlayingForPaused();
                weaponsToSilence.AddRange(turretWeapons);
                SilenceWeapons(weaponsToSilence);
                break;
            case 13:
                ResumePlayingForPaused();
                break;
            case 15:
                ResumePlayingForPaused();
                weaponsToSilence.AddRange(turretWeapons);
                SilenceWeapons(weaponsToSilence);
                break;
            case 16:
                ResumePlayingForPaused();
                weaponsToSilence.AddRange(turretWeapons);
                weaponsToSilence.AddRange(supportWeapons);
                SilenceWeapons(weaponsToSilence);
                break;
            default:
                //
                break;
        }
    }

    private void SilenceWeapons(List<Weapon> weaponsToSilence)
    {
        foreach (Weapon weapon in weaponsToSilence)
        {
            weapon.PausePlaying();
        }
    }

    private void ResumePlayingForPaused()
    {
        foreach (Weapon weapon in turretWeapons)
        {
            weapon.ResumePlaying();
        }
        foreach (Weapon weapon in cannonWeapons)
        {
            weapon.ResumePlaying();
        }
        foreach (Weapon weapon in supportWeapons)
        {
            weapon.ResumePlaying();
        }
    }

    public void AddWeaponToList(Weapon weapon)
    {
        switch (weapon.weaponType)
        {
            case WeaponType.Turret:
                turretWeapons.Add(weapon);
        break;
            case WeaponType.Cannon:
                cannonWeapons.Add(weapon);
                break;
            case WeaponType.Support:
                supportWeapons.Add(weapon);
                break;
            default:
                //
        break;
        }
    }

    public void SetVehicle(Vehicle vehicle)
    {
        chosenVehicle = vehicle;
        mainBPM = vehicle.vehicleMainBPM;
        ResetTriggerValue();
        OnVehicleSet();
    }

    public void ResetTriggerValue()
    {
        CalculateTriggerValue();
        allEnemiesManager.eventsAllowed = true;
        allEnemiesManager.TriggerEvent();
    }

    private void CalculateTriggerValue()
    {
        triggerValue = ((float)(60f / mainBPM) * numberOfTacts);
    }

    public void SetBossBPM(int newBMP)
    {
        oldBPM = mainBPM;
        mainBPM = newBMP;
        CalculateTriggerValue();
    }

    public void SetOldBMP()
    {
        mainBPM = oldBPM;
        CalculateTriggerValue();
    }

    public void StopAllWeaponMusic()
    {
        playingAllowed = false;
        OnMusicStopped();
    }

    public void ResumeAllWeaponMusic()
    {
        playingAllowed = true;
    }
}