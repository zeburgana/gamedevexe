using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    public float targetAngle;
    public float rotationSpeed = 1f;
    public bool canRotate = true;
    public bool isRotated; // is head finished rotating to its target angle

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        isRotated = (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, targetAngle)) == 0);
        if (canRotate && !isRotated)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed);
        }
    }
}
