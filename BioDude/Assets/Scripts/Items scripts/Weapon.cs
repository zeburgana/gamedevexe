using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public enum WeaponType
    {Melee, Pistol}

    public Sprite sprite;
    public GameObject projectile;
    public float projectileSpeed;
    public float cooldown;
    public Vector3 tip;
    public WeaponType projectileType;
    
    // Use this for initialization
	void Start () {
        tip = transform.GetChild(0).localPosition;
	}
	
}
