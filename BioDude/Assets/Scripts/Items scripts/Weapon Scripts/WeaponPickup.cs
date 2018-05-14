using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public int weaponID;
    public int ammoAmount;
    private WeaponManager weaponManager;

	// Use this for initialization
	void Start () {
        weaponManager = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>();     
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            weaponManager.AddAmmoByWeaponIndex(weaponID, ammoAmount);
            Destroy(gameObject);
        }

    }
}
