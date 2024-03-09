using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scripts should be placed on a weapon that would trigger wave effect in the environment
public class WaveEffector : MonoBehaviour
{
    public delegate void BeatPlayedAction();
    public event BeatPlayedAction OnBeatPlayed;

    /*[SerializeField] Weapon weapon;
    private void OnEnable()
    {
        weapon.SetWaveEffector(this);
    }*/

    public void TriggerBeatPlayedAction()
    {
        OnBeatPlayed();
    }
}
