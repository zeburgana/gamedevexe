using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon {
    private GuidedMisile guidedMisile;
    public float rotationSpeed;
    public float radius;
    public float force;
    // Use this for initialization
    void Start()
    {
        timeUntilSelfDestrucion = 2f;
    }


    public override void Equip(GameObject placeHolder) //shouldn't this be implemented in weaponor even in Item class?
    {
        guidedMisile = projectile.GetComponent<GuidedMisile>();
        SpriteRenderer spriteRend = placeHolder.GetComponent<SpriteRenderer>();
        spriteRend.sortingOrder = 2;
        spriteRend.sprite = sprite;
        maxAmmo = 10;
        clipSize = 1;
        currentClipAmmo = 1;
        currentAmmo = maxAmmo - currentClipAmmo;
        guidedMisile.setSpeed(projectileSpeed);
        guidedMisile.setRotationSpeed(rotationSpeed);
        guidedMisile.setRadius(radius);
        guidedMisile.setForce(force);
    }

    public override void Unequip()
    {

    }
}
