using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector2 sideMenuOpenedPosition;
    private Vector2 sideMenuClosedPosition;
    private void OnEnable()
    {
        sideMenuClosedPosition = rectTransform.anchoredPosition;

        iTween.ValueTo(gameObject, iTween.Hash(
        "from", rectTransform.anchoredPosition,
        "to", new Vector2(sideMenuOpenedPosition.x, sideMenuOpenedPosition.y),
        "time", 1f, "islocal", true, "easetype", iTween.EaseType.easeOutQuart,
        "onupdatetarget", this.gameObject,
        "onupdate", "MoveSideMenu"));
    }

    public void MoveSideMenu(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public void CloseSideMenu()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
        "from", rectTransform.anchoredPosition,
        "to", new Vector2(sideMenuClosedPosition.x, sideMenuClosedPosition.y),
        "time", 1f, "islocal", true, "easetype", iTween.EaseType.easeInQuart,
        "onupdatetarget", this.gameObject,
        "onupdate", "MoveSideMenu",
        "oncomplete", "ActionAfterTweenComplete"));
    }

    void ActionAfterTweenComplete()
    {
        gameObject.SetActive(false);
    }
}
