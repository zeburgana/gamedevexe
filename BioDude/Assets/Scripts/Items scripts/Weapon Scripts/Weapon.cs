using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public enum WeaponType
    {
        Melee,
        Pistol
    }

    public AudioClip weaponSound;
    public GameObject projectile;
    public GameObject tip;
    public float projectileSpeed;
    public float timeUntilSelfDestrucion;
    public float cooldown;
    public WeaponType projectileType;
    public int maxAmmo;
    public int currentAmmo;
    public int clipSize;
    public int currentClipAmmo;
    public float reloadTime;
    


}
