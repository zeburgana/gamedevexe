using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public enum WeaponType
    {
        Melee,
        Pistol,
        RocketLauncher
    }

    public AudioClip weaponSound;
    public GameObject projectile;
    public GameObject tip;
    public float projectileSpeed;
    public float timeUntilSelfDestrucion;
    public float cooldown;
    public WeaponType weaponType;
    public int maxAmmo;
    public int currentAmmo;
    public int clipSize;
    public int currentClipAmmo;
    public float reloadTime;
    public float damage;
}
