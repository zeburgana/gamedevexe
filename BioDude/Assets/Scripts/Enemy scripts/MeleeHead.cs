using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHead : MonoBehaviour {

    public float targetAngle;
    public float rotationSpeed = 0.5f;
    public bool canRotate = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(canRotate)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed);
    }
}
