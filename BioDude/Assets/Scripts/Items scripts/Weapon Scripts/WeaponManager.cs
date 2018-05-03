using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponSlotType
    {
        Player,
        Enemy
    }
    public WeaponSlotType weaponSlotType;
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
        GameObject.FindGameObjectWithTag(weaponSlotType.ToString()).GetComponent<Animator>().SetFloat("reloadTime", 1/activeWeapon.GetComponent<Weapon>().reloadTime);
        GameObject.FindGameObjectWithTag(weaponSlotType.ToString()).GetComponent<Animator>().SetTrigger("playerReload");
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

    public void Shoot()
    {
        //sometimes bullet spawns behind the player :D
        if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon != null && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().isReloading == false)
        {
            if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo > 0)
            {

                float x = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.x;
                float y = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.y;
                float z = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.z;
                GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = false;
                GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo--;
                StartCoroutine("Cooldown");
                Instantiate(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().projectile, new Vector3(x, y, z), transform.rotation);
                if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo == 0 && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentAmmo > 0)
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().Reload());
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().cooldown);
        GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = true;
    }

}
