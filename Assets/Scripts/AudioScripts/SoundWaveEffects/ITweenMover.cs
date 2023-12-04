using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITweenMover : MonoBehaviour
{
    [SerializeField] float animLength = .1f;
    [SerializeField] float jumpHeight = 2f;

    public void MoveChildObject()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", jumpHeight, "time", animLength, "islocal", true, "easetype", iTween.EaseType.easeOutQuint, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
    }

    void ActionAfterTweenComplete()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "time", animLength, "islocal", true, "easetype", iTween.EaseType.easeOutQuint));
    }
}
