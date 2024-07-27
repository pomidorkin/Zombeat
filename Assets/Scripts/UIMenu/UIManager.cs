using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;

public class UIManager : MonoBehaviour
{
    [SerializeField] SideMenu sideMenu;
    [SerializeField] Button burgerButton;
    [SerializeField] AnimatedIconHandler burgerButtonHandler;
    private bool SideMenuIsOpened = false;
    public void OpedSideMenu()
    {
        burgerButton.interactable = false;
        StartCoroutine(EnableButtorInteractive());
        if (!SideMenuIsOpened)
        {
            sideMenu.gameObject.SetActive(true);
            SideMenuIsOpened = true;
            burgerButtonHandler.PlayIn();
        }
        else
        {
            sideMenu.CloseSideMenu();
            SideMenuIsOpened = false;
            burgerButtonHandler.PlayOut();
        }
    }

    private IEnumerator EnableButtorInteractive()
    {
        yield return new WaitForSeconds(1f);
        burgerButton.interactable = true;
    }
}