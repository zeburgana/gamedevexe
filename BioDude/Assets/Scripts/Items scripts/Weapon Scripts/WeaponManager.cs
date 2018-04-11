using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject activeWeapon;
    public bool cooldownEnded = true;
    private Weapon weapon;

    // Use this for initialization
    void Start ()
    {
        if (activeWeapon != null)
        {
            weapon = activeWeapon.GetComponent<Weapon>();
            weapon.Equip(gameObject);
        }
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        activeWeapon = newWeapon;
        weapon = activeWeapon.GetComponent<Weapon>();
        //weapon.sprite;
        weapon.Equip(gameObject);
    }
}
