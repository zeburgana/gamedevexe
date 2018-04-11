using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject activeWeapon;
    public Weapon weapon;
    public bool cooldownEnded = true;

	// Use this for initialization
	void Start ()
    {
        weapon = activeWeapon.GetComponent<Weapon>();
    	weapon.Equip();
    }
}
