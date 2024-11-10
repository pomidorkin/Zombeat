using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_BOSS_ACTIVATOR : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] bool activateBoss = false;
    // Update is called once per frame
    void Update()
    {
        if (activateBoss)
        {
            activateBoss = false;
            if (enemy.humanoidBossManager.equipAnim != false)
            {
                enemy.humanoidBossManager.equipAnim.PerformShoot();
                StartCoroutine(ActivateBossCoroutine());
            }
            else
            {
                enemy.noticedThePlayer = true;
            }
        }
    }

    private IEnumerator ActivateBossCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        enemy.noticedThePlayer = true;
    }
}