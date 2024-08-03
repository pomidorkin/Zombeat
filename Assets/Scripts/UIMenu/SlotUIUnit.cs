using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUIUnit : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image weaponImage;

    public void SetWeaponImageVisible(bool val)
    {
        weaponImage.gameObject.SetActive(val);
    }

    public void SetWeaponImage(Sprite image)
    {
        weaponImage.sprite = image;
    }
}
