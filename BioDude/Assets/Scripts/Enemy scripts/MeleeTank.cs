using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Children order is important fro this script to work

public class MeleeTank : MonoBehaviour
{

    Transform legs;
    Transform head;

    public GameObject player;

	// Use this for initialization
	void Start () {
        legs = transform.GetChild(0);
        head = transform.GetChild(1);
        
        
	}
	
	// Update is called once per frame
	void Update () {
       
    }
}
