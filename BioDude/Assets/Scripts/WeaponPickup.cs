using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public GameObject[] weapons;
    public GameObject currentSpawnedWeapon;
    bool pickedUp;

	// Use this for initialization
	void Start () {
        currentSpawnedWeapon = weapons[Random.Range(0, weapons.Length)];
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
            other.transform.Find("WeaponSlot").GetComponent<WeaponManager>().UpdateWeapon(currentSpawnedWeapon);
            pickedUp = true;
        }
    }
}
