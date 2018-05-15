using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour {


    public int grenadeID;
    public int count = 2;
    private WeaponManager weaponManager;
    bool pickedUp;

    // Use this for initialization
    void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            weaponManager.AddExplosivesByIndex(grenadeID, count);
        }

    }
}
