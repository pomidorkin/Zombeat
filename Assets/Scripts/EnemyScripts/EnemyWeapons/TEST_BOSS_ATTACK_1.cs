using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_BOSS_ATTACK_1 : EnemyWeapon
{
    public override void AbortShoot()
    {
    }

    public override void PerformShoot()
    {
        Debug.Log("Boss Performing Attack N.1");
        ReturnBackToChase();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}