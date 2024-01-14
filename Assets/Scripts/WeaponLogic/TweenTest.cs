using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    [SerializeField] float reationSpeed = 0.05f;
    [SerializeField] float statPost = 0.1140011f;
    [SerializeField] float targetPos = 0.033f;
    [SerializeField] ParticleSystem[] particleSystems;
    private bool particleSystemsActivated = false;
    public void TriggerTween()
    {
        iTween.MoveTo(gameObject, iTween.Hash("z", targetPos, "time", reationSpeed, "islocal", true, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
        if (!particleSystemsActivated)
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.gameObject.SetActive(true);
            }
            particleSystemsActivated = true;
        }
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }

    void ActionAfterTweenComplete()
    {
        iTween.MoveTo(gameObject, iTween.Hash("z", statPost, "time", reationSpeed, "islocal", true));
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
    }
}
