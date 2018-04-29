using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoManager : MonoBehaviour {

    public int currentClipAmmo;
    public int currentAmmo;

	// Use this for initialization
	void Start () {
        currentClipAmmo = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo;
        currentAmmo = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentAmmo;

    }
	
	// Update is called once per frame
	void Update () {
        currentClipAmmo = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo;
        currentAmmo = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentAmmo;
    }
}
