﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allerting : MonoBehaviour {

    public uint howManySeeMe = 0;
    private Transform PLKP; //PlayerLastknownPosition


    // code for allerting enemies in area if shoot if walk near (if needed) can be a skill
    // while pursuing maybe area around player should catch non alerted enemy and allert them

    // this script should watch all gun shots and set playerLastKnownPosition i dabartine position ir alertinti visus aplinkinius enemy
    // o enmey skripte jei enemy priima damage tada ji reikia alertinti

	// Use this for initialization
	void Start () {
        PLKP = GameObject.Find("PlayerLastKnownPosition").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AllertSurroundings(float radius)
    {
        PLKP.position = transform.position;
        RaycastHit2D[] objects = Physics2D.CircleCastAll(transform.position, radius, Vector2.right, radius, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit2D obj in objects)
        {
            obj.transform.GetComponent<Tank>().PursuePlayer();
        }
    }



}
