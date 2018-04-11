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

	public override void Equip()
	{
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        GetComponent<SpriteRenderer>().sprite = sprite;
        tip = transform.localPosition;
	}

	public override void Unequip()
	{

	}
}
