using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPopupUI : MonoBehaviour
{
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;
    [SerializeField] public Weapon attachedWeapon;
    [SerializeField] Slider volumeSlider;
    [SerializeField] WeaponRemover weaponRemover;
    [SerializeField] int weaponDamage;
    [SerializeField] public TMP_Text damageText;
    [SerializeField] public TMP_Dropdown preferredEnemyType;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;


        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        preferredEnemyType.onValueChanged.AddListener(delegate { DropdownValueChangeCheck(); });
        DropdownHandler();
    }

    public void DropdownHandler()
    {
        // Dropdown Logic
        preferredEnemyType.ClearOptions();
        List<string> dropdownOptions = new List<string>();
        for (int i = 0; i < System.Enum.GetValues(typeof(EnemyType)).Length; i++)
        {
            dropdownOptions.Add("Enemy Type " + i);
        }
        preferredEnemyType.AddOptions(dropdownOptions);

        preferredEnemyType.value = (int)attachedWeapon.weaponSaveData.preferredEnemy;
        attachedWeapon.weaponTracking.interestedType = (EnemyType)preferredEnemyType.value;



        // Volume Logic
        volumeSlider.value = attachedWeapon.weaponSaveData.weaponSoundVolume;
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        attachedWeapon.ChangeVolume(volumeSlider.value);
    }

    public void DropdownValueChangeCheck()
    {
        attachedWeapon.weaponTracking.interestedType = (EnemyType)preferredEnemyType.value;
        attachedWeapon.weaponSaveData.preferredEnemy = (EnemyType)preferredEnemyType.value;
        Progress.Instance.Save();
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
        attachedWeapon.SaveChangedVolume(volumeSlider.value);
        gameObject.SetActive(false);
    }
}
