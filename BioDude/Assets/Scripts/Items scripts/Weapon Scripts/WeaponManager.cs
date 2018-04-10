using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject activeWeapon;
    Weapon weapon;
    public bool cooldownEnded = true;

	// Use this for initialization
	void Start () {
        weapon = activeWeapon.GetComponent<Weapon>();
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;
        weapon.tip = transform.GetChild(0).localPosition;
    }

    void Equip () {
        
    }
}
