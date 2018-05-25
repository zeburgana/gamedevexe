using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

	public string weaponName;
	public int ammoAmount;
	private WeaponManager weaponManager;

	// Use this for initialization
	void Start () {
		weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>();     
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			weaponManager.AddAmmoByName(weaponName, ammoAmount);

            Destroy(gameObject);
		}
	}
}
