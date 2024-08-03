using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;

public class VehicleSelectionUnit : MonoBehaviour
{
    [SerializeField] public VehicleManager vehicleManager;
    [SerializeField] public Image carImage;
    [SerializeField] public ProgressBar carHealthProgressBar;
    [SerializeField] public ProgressBar carSpeedProgressBar;
    [SerializeField] TMP_Text carName;
    [SerializeField] GameObject[] unitElements;
    public bool selected = false;
    //[SerializeField] public Button button;
    public int vehicleId; //Progress.Instance.playerInfo.vehicleSaveDatas[i].idVehicle

    public void SelectVehicle()
    {
        vehicleManager.selectedVehicleId = vehicleId;
    }

    public void AnimateUnitElemets()
    {
        selected = true;
        iTween.ScaleTo(unitElements[0], iTween.Hash("x", 1, "y", 1, "time", 0.5, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
        StartCoroutine(AnimateElement(1, 0.25f));
        StartCoroutine(AnimateElement(2, 0.5f));
    }

    private IEnumerator AnimateElement(int id, float delay)
    {
        yield return new WaitForSeconds(delay);
        iTween.ScaleTo(unitElements[id], iTween.Hash("x", 1, "y", 1, "time", 0.5, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
    }

    public void AnimateUnitElemetsBack()
    {
        selected = false;
        iTween.ScaleTo(unitElements[0], iTween.Hash("x", 0, "y", 0, "time", 0.5, "easetype", iTween.EaseType.easeInCirc, "islocal", true));
        StartCoroutine(AnimateElementBack(1, 0.25f));
        StartCoroutine(AnimateElementBack(2, 0.5f));
    }

    private IEnumerator AnimateElementBack(int id, float delay)
    {
        yield return new WaitForSeconds(delay);
        iTween.ScaleTo(unitElements[id], iTween.Hash("x", 0, "y", 0, "time", 0.5, "easetype", iTween.EaseType.easeInCirc, "islocal", true));
    }
}
