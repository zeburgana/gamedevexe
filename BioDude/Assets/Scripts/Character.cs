using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public const float healthMax = 100;
	public float healthCurrent;  // set to private 


    // Use this for initialization
    void Start ()
	{
		SetMaxHealth();
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
            gameObject.SetActive(false);
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
