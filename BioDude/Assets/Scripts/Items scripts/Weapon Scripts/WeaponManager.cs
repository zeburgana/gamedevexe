using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponWielderType
    {
        Player,
        Enemy
    }
    public WeaponWielderType weaponWielderType;
    private GameObject weaponSlotType;
    private GameObject reloadObject;
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
            Debug.Log(weaponWielderType.ToString() + "WeaponSlot");
            weaponSlotType = GameObject.FindGameObjectWithTag(weaponWielderType.ToString() + "WeaponSlot");
            reloadObject = GameObject.FindGameObjectWithTag(weaponWielderType.ToString());
        }
    }


    public void UpdateWeapon(GameObject newWeapon)  // BULLSHIT kam tiek daug kartu reikia getcomponent
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
        weaponSlotType = GameObject.FindGameObjectWithTag(weaponWielderType.ToString() + "WeaponSlot");
        reloadObject = GameObject.FindGameObjectWithTag(weaponWielderType.ToString());
    }

    public IEnumerator Reload() // BULLSHIT kam tiek daug kartu reikia getcomponent
    {
        isReloading = true;
        reloadObject.GetComponent<Animator>().SetFloat("reloadTime", 1/activeWeapon.GetComponent<Weapon>().reloadTime);
        reloadObject.GetComponent<Animator>().SetTrigger("playerReload");
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

    public void Shoot() // BULLSHIT kam tiek daug kartu reikia getcomponent
    {
        //sometimes bullet spawns behind the player :D
        if (weaponSlotType.GetComponent<WeaponManager>().cooldownEnded && weaponSlotType.GetComponent<WeaponManager>().activeWeapon != null && weaponSlotType.GetComponent<WeaponManager>().isReloading == false)
        {
            if (weaponSlotType.GetComponent<WeaponManager>().currentClipAmmo > 0)
            {
                Vector3 projectileVector = weaponSlotType.transform.position;
                weaponSlotType.GetComponent<WeaponManager>().cooldownEnded = false;
                weaponSlotType.GetComponent<WeaponManager>().currentClipAmmo--;
                StartCoroutine("Cooldown");
                Instantiate(weaponSlotType.GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().projectile, projectileVector, transform.rotation);
                if (weaponSlotType.GetComponent<WeaponManager>().currentClipAmmo == 0 && weaponSlotType.GetComponent<WeaponManager>().currentAmmo > 0)
                    StartCoroutine(weaponSlotType.GetComponent<WeaponManager>().Reload());
            }
        }
    }

    IEnumerator Cooldown() // BULLSHIT kam tiek daug kartu reikia getcomponent
    {
        yield return new WaitForSeconds(weaponSlotType.GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().cooldown);
        weaponSlotType.GetComponent<WeaponManager>().cooldownEnded = true;
    }

}
