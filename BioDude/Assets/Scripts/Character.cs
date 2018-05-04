using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public const float healthMax = 100;
	public float healthCurrent;


	// Use this for initialization
	void Start ()
	{
		SetMaxHealth();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (healthCurrent <= 0)
        {
            gameObject.SetActive(false);
        }
	}

	public void Damage(float amount)
	{
		healthCurrent -= amount;
	}

	public void Heal(float amount)
	{
		healthCurrent += amount;
	}

	public void SetMaxHealth()
	{
		healthCurrent = healthMax;
	}
}
