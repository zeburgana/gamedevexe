﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public Sprite sprite;
	
	public abstract void Equip ();
	public abstract void Unequip ();
}
