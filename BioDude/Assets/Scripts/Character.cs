﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    public float healthMax { get; protected set; }
	public float healthCurrent { get; protected set; } 

    protected abstract void Die(); // override

    // Use this for initialization
    protected virtual void Start ()
	{
		SetMaxHealth(); //check if this even work if nested
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Damage(float amount)
	{
		healthCurrent -= amount;
        if (healthCurrent <= 0)
        {
            healthCurrent = 0;
            Die();
        }
    }

	public void Heal(float amount)
	{
		healthCurrent += Mathf.Min(amount, healthMax);
    }

	public void SetMaxHealth()
	{
		healthCurrent = healthMax;
	}
}