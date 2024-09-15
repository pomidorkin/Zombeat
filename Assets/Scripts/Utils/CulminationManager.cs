using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CulminationManager : MonoBehaviour
{
    [SerializeField] OrchestraManager orchestraManager;
    [SerializeField] private Camera camera;
    [SerializeField] MaterialPropertyController speedVFX;

    private float timeToMoveCameraAway = 0.25f;
    private float timeToMoveCameraback = 2.0f;
    private void OnEnable()
    {
        orchestraManager.OnCulminationPlayed += CulminationHandler;
        //camera = FindAnyObjectByType<Camera>();
    }

    private void OnDisable()
    {
        orchestraManager.OnCulminationPlayed -= CulminationHandler;
    }

    private void CulminationHandler()
    {
        iTween.MoveTo(camera.gameObject, iTween.Hash("z", -2, "time", timeToMoveCameraAway, "islocal", true, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "ActionAfterTweenComplete", "oncompletetarget", gameObject));
        // Some kind of effect should be played here
        /*speedVFX.playingAllowed = true;
        StartCoroutine(DisableSpeedVFX());*/

    }

    void ActionAfterTweenComplete()
    {
        iTween.MoveTo(camera.gameObject, iTween.Hash("z", 0, "time", timeToMoveCameraback, "islocal", true, "easetype", iTween.EaseType.easeOutCubic));
    }

    /*private IEnumerator DisableSpeedVFX()
    {
        yield return new WaitForSeconds(timeToMoveCameraback + timeToMoveCameraAway);
        speedVFX.playingAllowed = false;

    }*/
}
