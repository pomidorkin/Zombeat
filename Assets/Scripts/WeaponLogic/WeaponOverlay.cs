using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverlay : MonoBehaviour
{
    [SerializeField] Material myMaterial;
    [SerializeField] Color topColorForbidden;
    [SerializeField] Color bottomColorForbidden;
    [SerializeField] Color topColorAllowed;
    [SerializeField] Color bottomColorAllowed;
    [SerializeField] Color defaultColor;
    private List<Collider> collidingWeapons;

    public bool isBeingPlaced = false;
    public bool isOverlaying = false;

    private void Start()
    {
        if (!isBeingPlaced)
        {
            SetColorToDefault();
        }
        collidingWeapons = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBeingPlaced)
        {
            if (other.tag == "Weapon")
            {
                SetColorToForbidden();
                collidingWeapons.Add(other);
                isOverlaying = true;
                Debug.Log("I am colliding with " + collidingWeapons.Count + " other weapons");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBeingPlaced)
        {
            if (other.tag == "Weapon" && collidingWeapons.Contains(other))
            {
                collidingWeapons.Remove(other);
                if (collidingWeapons.Count <= 0)
                {
                    SetColorToAllowed();
                    isOverlaying = false;
                }

                Debug.Log("I am colliding with " + collidingWeapons.Count + " other weapons");
            }
        }
    }

    public void SetColorToDefault()
    {
        myMaterial.SetColor("_TopColor", defaultColor);
        myMaterial.SetColor("_BottomColor", defaultColor);
    }

    private void SetColorToForbidden()
    {
        myMaterial.SetColor("_TopColor", topColorForbidden);
        myMaterial.SetColor("_BottomColor", bottomColorForbidden);
    }
    public void SetColorToAllowed()
    {
        myMaterial.SetColor("_TopColor", topColorAllowed);
        myMaterial.SetColor("_BottomColor", bottomColorAllowed);
    }
}
