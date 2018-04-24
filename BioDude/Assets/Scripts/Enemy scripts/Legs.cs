using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour {

    public float targetAngle;
    public float rotationSpeed = 0.5f;
    public bool canRotate = true;
    Transform tank;
    Grid test;

	// Use this for initialization
	void Start () {
        tank = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if(canRotate)
            tank.rotation = Quaternion.RotateTowards(tank.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed);
    }

    // position - set position your tank should go to.
    public void GoTo(Vector2 positiion)
    {
        
    }


}
