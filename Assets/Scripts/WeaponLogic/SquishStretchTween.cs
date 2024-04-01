using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishStretchTween : MonoBehaviour
{
    [SerializeField] float duration = 0.8f;
    [SerializeField] float startScale = 1.0f;
    [SerializeField] float targetSquishScale = .8f;
    [SerializeField] float targetStretchScale = 1.2f;
    [SerializeField] iTween.EaseType startTweenType;
    [SerializeField] iTween.EaseType endTweenType;

    public void TriggerTween()
    {
        /*iTween.ScaleTo(gameObject, iTween.Hash("z", targetSquishScale, "time", duration / 2f, "islocal", true, "easetype", startTweenType, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
        iTween.ScaleTo(gameObject, iTween.Hash("x", targetSquishScale, "time", duration / 2f, "islocal", true, "easetype", startTweenType));
        iTween.ScaleTo(gameObject, iTween.Hash("y", targetStretchScale, "time", duration / 2f, "islocal", true, "easetype", startTweenType));*/
        iTween.ScaleTo(gameObject, iTween.Hash("x", targetSquishScale, "y", targetStretchScale, "z", targetSquishScale, "time", duration * .1f, "islocal", true, "easetype", startTweenType, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
    }

    void ActionAfterTweenComplete()
    {
        //iTween.ScaleTo(gameObject, iTween.Hash("z", startScale, "time", duration / 2f, "islocal", true, "easetype", endTweenType));
        iTween.ScaleTo(gameObject, iTween.Hash("x", startScale, "y", startScale, "z", startScale, "time", duration * .9f, "islocal", true, "easetype", endTweenType));
        //iTween.ScaleTo(gameObject, iTween.Hash("y", startScale, "time", duration / 2f, "islocal", true, "easetype", endTweenType));
    }
}
