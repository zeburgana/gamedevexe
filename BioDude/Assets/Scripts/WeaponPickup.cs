using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public GameObject weapon;
    private GameObject spawnedWeapon;
    private WeaponManager weaponManager;
    bool pickedUp;

	// Use this for initialization
	void Start () {
       spawnedWeapon = weapon;
        weaponManager = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>();     
    }
	
	// Update is called once per frame
	void Update () {
		if(pickedUp) // destroy should be called in OnTriggerEnter method
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (weaponManager.weaponArray[0] == null)
            {
                other.transform.Find("WeaponSlot").GetComponent<WeaponManager>().UpdateWeapon(spawnedWeapon);
                weaponManager.weaponArray[0] = spawnedWeapon;
                pickedUp = true;
            }
            else if (weaponManager.weaponArray[0] != null && weaponManager.weaponArray[1] == null)
            {
                other.transform.Find("WeaponSlot").GetComponent<WeaponManager>().UpdateWeapon(spawnedWeapon);
                weaponManager.weaponArray[1] = spawnedWeapon;
                pickedUp = true;
            }
        }
    }
}
