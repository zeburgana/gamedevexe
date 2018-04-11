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
}
