using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weaponArray;
    public GameObject activeWeapon;
    public GameObject activeWeaponTip;
    public bool cooldownEnded = true;
    public bool isReloading = false;
    private Weapon weapon;
    public int currentAmmo;
    public int maxAmmo;
    public int clipSize;
    public int currentClipAmmo;

    // Use this for initialization
    void Start ()
    {
        if (activeWeapon != null)
        {
            weapon = activeWeapon.GetComponent<Weapon>();
            weapon.currentAmmo = weapon.currentAmmo - weapon.currentClipAmmo;
            currentAmmo = weapon.currentAmmo;
            maxAmmo = weapon.maxAmmo;
            clipSize = weapon.clipSize;
            currentClipAmmo = weapon.currentClipAmmo;
            weapon.Equip(gameObject);
        }
    }


    public void UpdateWeapon(GameObject newWeapon)
    {
        activeWeapon = newWeapon;
        currentAmmo = activeWeapon.GetComponent<Weapon>().currentAmmo;
        maxAmmo = activeWeapon.GetComponent<Weapon>().maxAmmo;
        weapon = activeWeapon.GetComponent<Weapon>();
        clipSize = activeWeapon.GetComponent<Weapon>().clipSize;
        currentClipAmmo = activeWeapon.GetComponent<Weapon>().currentClipAmmo;
        activeWeaponTip = activeWeapon.GetComponent<Weapon>().tip;
        //weapon.sprite;
        weapon.Equip(gameObject);
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("reloadTime", 1/activeWeapon.GetComponent<Weapon>().reloadTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("playerReload");
        yield return new WaitForSeconds(activeWeapon.GetComponent<Weapon>().reloadTime);
        if(currentAmmo + currentClipAmmo >= clipSize)
        {
            currentAmmo = currentAmmo - (clipSize - currentClipAmmo);
            currentClipAmmo = clipSize;
        }

        else if(currentAmmo + currentClipAmmo < clipSize)
        {
            currentClipAmmo = currentAmmo + currentClipAmmo;
            currentAmmo = 0;
        }
        isReloading = false;
    }

}
