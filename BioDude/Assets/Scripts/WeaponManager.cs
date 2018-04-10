using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject activeWeapon = null;
    Weapon weapon;
    public bool cooldownEnded = true;

	// Use this for initialization
	void Start () {
        if (activeWeapon != null)
        {
            weapon = activeWeapon.GetComponent<Weapon>();
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            GetComponent<SpriteRenderer>().sprite = weapon.sprite;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void UpdateWeapon(GameObject newWeapon)
    {
        activeWeapon = newWeapon;
        weapon = activeWeapon.GetComponent<Weapon>();
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;
    }
}
