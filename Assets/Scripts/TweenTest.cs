using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    [SerializeField] float reationSpeed = 0.05f;
    [SerializeField] float statPost = 0.1140011f;
    [SerializeField] float targetPos = 0.033f;
    public void TriggerTween()
    {
        iTween.MoveTo(gameObject, iTween.Hash("z", targetPos, "time", reationSpeed, "islocal", true, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
    }

    void ActionAfterTweenComplete()
    {
        iTween.MoveTo(gameObject, iTween.Hash("z", statPost, "time", reationSpeed, "islocal", true));
    }
}
