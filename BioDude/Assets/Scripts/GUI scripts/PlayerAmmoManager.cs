using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoManager : MonoBehaviour {

    public int currentClipAmmo;
    public int currentAmmo;
    private Weapon weapon;

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon != null)
        {
            weapon = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>();
            currentClipAmmo = weapon.currentClipAmmo;
            currentAmmo = weapon.currentAmmo;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon != null)
        {
            weapon = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>();
            currentClipAmmo = weapon.currentClipAmmo;
            currentAmmo = weapon.currentAmmo;
        }
    }
}
