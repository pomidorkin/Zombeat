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
            enemy.noticedThePlayer = true;
        }
    }
}
