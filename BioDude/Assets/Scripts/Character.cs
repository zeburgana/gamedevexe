using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	const float healthMax = 100;
	float healthCurrent;


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

	public void DealDamage(int amount)
	{
		healthCurrent -= amount;
	}

	public void Heal(int amount)
	{
		healthCurrent += amount;
	}

	public void SetMaxHealth()
	{
		healthCurrent = healthMax;
	}
}
