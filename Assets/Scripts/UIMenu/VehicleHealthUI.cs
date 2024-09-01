using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private float currentValue;
    [SerializeField] Image carImage;

    private Vehicle vehicle;

    private void Start()
    {
        currentValue = 1;
        healthBar.value = currentValue;
        if (vehicle == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("WeaponBase");
            vehicle = target.GetComponentInParent<Vehicle>();
            vehicle.SetHealthIU(this);
            carImage.sprite = vehicle.carImage;
        }
    }

    public void SetHealthBarValue(float maxHealth, float currectHealth)
    {
        float newVal = (currectHealth / maxHealth);
        //healthBar.currentPercent = newVal;
        Debug.Log("newVal: " + newVal + ", currectHealth: " + currectHealth + "\nnewVal: " + newVal + ", currentValue: " + currentValue + "\n(currectHealth / maxHealth): " + (currectHealth / maxHealth));
        TweenVariableExample(currentValue, newVal, maxHealth);
        currentValue = newVal;
        
    }

    void TweenVariableExample(float initialValue, float targetValue, float maxHealth)
    {
        // Count to 100 over 3 seconds:
        iTween.ValueTo(gameObject, iTween.Hash("from", initialValue, "to", targetValue, "time", .5f, "onupdatetarget", gameObject, "onupdate", "UpdateCounter", "easetype", iTween.EaseType.easeOutQuint));
    }

    void UpdateCounter(float newValue)
    {
        healthBar.value = newValue;
    }
}
