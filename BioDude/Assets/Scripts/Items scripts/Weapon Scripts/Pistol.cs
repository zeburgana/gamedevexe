using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

	// Use this for initialization
	void Start ()
	{
		timeUntilSelfDestrucion = 2f;
	}

	public override void Fire()
	{
	}

	public override void Equip(GameObject placeHolder) //shouldn't this be implemented in weaponor even in Item class?
	{
        SpriteRenderer spriteRend = placeHolder.GetComponent<SpriteRenderer>();
        spriteRend.sortingOrder = 2;
        spriteRend.sprite = sprite;
        tip = transform.localPosition;
	}

	public override void Unequip()
	{

	}
}
