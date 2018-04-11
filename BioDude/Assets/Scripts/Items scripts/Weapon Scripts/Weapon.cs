using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum WeaponType
    {
        Melee,
        Pistol
    }

    public GameObject projectile;
    public float projectileSpeed;
    public float timeUntilSelfDestrucion;
    public float cooldown;
    public Vector3 tip;
    public WeaponType projectileType;
    
    public virtual void Fire() {}
}
