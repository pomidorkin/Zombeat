using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPopupUI : MonoBehaviour
{
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;
    [SerializeField] WeaponRemover weaponRemover;
    [SerializeField] int weaponDamage;
    [SerializeField] public TMP_Text damageText;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAt != null)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

            if (transform.position != pos)
            {
                transform.position = pos;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void RemoveWeapon()
    {
        weaponRemover.RemoveWeapon();
        gameObject.SetActive(false);
    }

    public void HidePopUpMenu()
    {
        gameObject.SetActive(false);
    }
}
