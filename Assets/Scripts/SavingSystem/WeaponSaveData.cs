using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSaveData
{
    //[SerializeField] GameObject weaponPrefab;
    public int id;
    public bool obtained;
    public bool placed;
    public int idVehicle;
    /*public bool isWaveEffector = false;
    public int BPM;
    public SoundUnitKey soundUnitKey;*/
    // Position on a car;
    public Vector3 position;
    public Quaternion rotation;
    public float childRotationY;
    public float weaponSoundVolume = 1f;
    public EnemyType preferredEnemy = EnemyType.Default;
}