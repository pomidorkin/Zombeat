using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGarage : MonoBehaviour
{
    [SerializeField] float height = 5.0f;
    [SerializeField] float duration = 3.0f;
    [SerializeField] float initialYPos;
    [SerializeField] GameObject childObject;

    private void Start()
    {
        initialYPos = transform.localPosition.y;
    }
    public void TriggerGarageMovement()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", height, "time", duration, "islocal", true, "easetype", iTween.EaseType.easeOutBack, "oncomplete", "ActionAfterRaiseComplete", "oncompletetarget", gameObject));
    }

    void ActionAfterRaiseComplete()
    {
        iTween.RotateTo(gameObject, iTween.Hash("x", 180, "time", duration, "islocal", true, "easetype", iTween.EaseType.easeInOutBack, "oncomplete", "ActionAfterRotation", "oncompletetarget", gameObject));
        iTween.RotateTo(childObject, iTween.Hash("y", 180, "time", duration, "islocal", true, "easetype", iTween.EaseType.easeInOutBack));
    }

    void ActionAfterRotation()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", initialYPos, "time", duration, "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
    }
}
