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
    public class itemStruct
    {
        public GameObject item;
        public int itemCount;
        public itemStruct(GameObject obj, int count = 0)
        {
            item = obj;
            itemCount = count;
        }

    }
    public WeaponWielderType weaponWielderType;
    private WeaponManager weaponManager;
    private GameObject weaponSlotType;
    private GameObject reloadObject;
    [HideInInspector]
    public itemStruct[] GrenadeArray;
    public itemStruct activeGrenade;
    public GameObject[] weaponArray;
    public GameObject activeWeapon;
    public GameObject activeWeaponTip;
    public bool cooldownEnded = true;
    public bool isReloading = false;
    private Weapon weapon;
    private int currentAmmo;
    private int maxAmmo;
    private int clipSize;
    private int currentClipAmmo;
    private SpriteRenderer spriteRenderer;



    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponArray = new GameObject[2];
        weaponSlotType = GameObject.FindGameObjectWithTag(weaponWielderType.ToString() + "WeaponSlot");
        weaponManager = weaponSlotType.GetComponent<WeaponManager>();
        reloadObject = GameObject.FindGameObjectWithTag(weaponWielderType.ToString());
        if (activeWeapon != null)
        {
            weapon = activeWeapon.GetComponent<Weapon>();
            weaponArray[0] = activeWeapon;
            weapon.currentAmmo = weapon.currentAmmo - weapon.currentClipAmmo;
            currentAmmo = weapon.currentAmmo;
            maxAmmo = weapon.maxAmmo;
            clipSize = weapon.clipSize;
            currentClipAmmo = weapon.currentClipAmmo;
            weapon.Equip(gameObject);
            Debug.Log(weaponWielderType.ToString() + "WeaponSlot");
        }
    }


    public void SwitchWeapon()
    {
        if (!isReloading)
        {
            if (activeWeapon == weaponArray[0] && weaponArray[1] != null)
            {
                activeWeapon = weaponArray[1];
                weapon = activeWeapon.GetComponent<Weapon>();
                currentAmmo = weapon.currentAmmo;
                maxAmmo = weapon.maxAmmo;
                clipSize = weapon.clipSize;
                currentClipAmmo = weapon.currentClipAmmo;
                activeWeaponTip.transform.localPosition = weapon.tip.transform.localPosition;
                spriteRenderer.sprite = weaponArray[1].GetComponent<SpriteRenderer>().sprite;


            }
            else if (activeWeapon == weaponArray[1] && weaponArray[0] != null)
            {
                activeWeapon = weaponArray[0];
                weapon = activeWeapon.GetComponent<Weapon>();
                currentAmmo = weapon.currentAmmo;
                maxAmmo = weapon.maxAmmo;
                clipSize = weapon.clipSize;
                currentClipAmmo = weapon.currentClipAmmo;
                activeWeaponTip.transform.localPosition = weapon.tip.transform.localPosition;
                spriteRenderer.sprite = weaponArray[0].GetComponent<SpriteRenderer>().sprite;
            }

            if (weapon.currentClipAmmo <= 0)
                Reload();
        }
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        activeWeapon = newWeapon;
        weapon = activeWeapon.GetComponent<Weapon>();
        currentAmmo = weapon.currentAmmo;
        maxAmmo = weapon.maxAmmo;
        clipSize = weapon.clipSize;
        currentClipAmmo = weapon.currentClipAmmo;
        activeWeaponTip.transform.localPosition = weapon.tip.transform.localPosition;
        weapon.Equip(gameObject);
    }

    public IEnumerator Reload()
    {
        Animator animator = reloadObject.GetComponent<Animator>();
        isReloading = true;
        animator.SetFloat("reloadTime", 1/weapon.reloadTime);
        animator.SetTrigger("playerReload");
        yield return new WaitForSeconds(weapon.reloadTime);
        if(weapon.currentAmmo + weapon.currentClipAmmo >= weapon.clipSize)
        {
            weapon.currentAmmo = weapon.currentAmmo - (weapon.clipSize - weapon.currentClipAmmo);
            weapon.currentClipAmmo = weapon.clipSize;
        }

        else if(weapon.currentAmmo + weapon.currentClipAmmo < weapon.clipSize)
        {
            weapon.currentClipAmmo = weapon.currentAmmo + weapon.currentClipAmmo;
            weapon.currentAmmo = 0;
        }
        isReloading = false;
    }

    public void Shoot()
    {
        if (weaponManager.cooldownEnded && weaponManager.activeWeapon != null && weaponManager.isReloading == false)
        {
            if (weapon.currentClipAmmo > 0)
            {
                Vector3 projectileVector = weaponSlotType.transform.position;
                weaponManager.cooldownEnded = false;
                weapon.currentClipAmmo--;
                StartCoroutine("Cooldown");
                //Instantiate(weapon.projectile, projectileVector, transform.rotation);
                Instantiate(weapon.projectile, activeWeaponTip.transform.position, transform.rotation);
                if (weapon.currentClipAmmo == 0 && weapon.currentAmmo > 0)
                    StartCoroutine(weaponManager.Reload());
            }
        }
    }
    public void AddGrenade(GameObject grenade, int count)
    {
        int currentGrenade = -1;

        for (int i = 0; i < GrenadeArray.Length; i++)
        {
            if (activeGrenade == GrenadeArray[i])
            {
                currentGrenade = i;
                break;
            }
        }
        if (currentGrenade > -1 && currentGrenade <= GrenadeArray.Length)
        {
            if (currentGrenade == GrenadeArray.Length)
                currentGrenade = -1;
            GrenadeArray[currentGrenade + 1].itemCount += count;
        }
        else
        {
            //GrenadeArray[] find index to put grenade
        }
    }
    public void SwitchGrenade()
    {
        int currentGrenade = -1;

        for (int i = 0; i < GrenadeArray.Length; i++)
        {
            if (activeGrenade == GrenadeArray[i])
            {
                currentGrenade = i;
                break;
            }
        }
        if (currentGrenade > -1 && currentGrenade <= GrenadeArray.Length)
        {
            if (currentGrenade == GrenadeArray.Length)
                currentGrenade = -1;
            activeGrenade = GrenadeArray[currentGrenade + 1];
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(weapon.cooldown);
        weaponManager.cooldownEnded = true;
    }

}
