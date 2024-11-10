using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] int bossId;
    [SerializeField] int associatedVehicleId;
    [SerializeField] VehicleContainer vehicleContainer;
    public void BothDeathHandler()
    {
        if (Progress.Instance.playerInfo.defeatedBosses.Count == 0)
        {
            AddNewDefeatedBoss();
        }
        else
        {
            bool bossFound = false;
            foreach (BossDictionarySerializable bossDictionary in Progress.Instance.playerInfo.defeatedBosses)
            {
                if (bossDictionary.bossId == bossId)
                {
                    bossFound = true;
                    if (!bossDictionary.isDefeated)
                    {
                        bossDictionary.isDefeated = true;
                    }
                    break;
                }
            }
            if (!bossFound)
            {
                AddNewDefeatedBoss();
            }
        }
    }

    private void AddNewDefeatedBoss()
    {
        BossDictionarySerializable bossDic = new BossDictionarySerializable();
        bossDic.bossId = bossId;
        bossDic.isDefeated = true;
        Progress.Instance.playerInfo.defeatedBosses.Add((bossDic));
        vehicleContainer.ObtainNewVehicle(associatedVehicleId);
        // Here you should make a car pop-up and give the player a new car
        //Progress.Instance.Save();
    }
}
